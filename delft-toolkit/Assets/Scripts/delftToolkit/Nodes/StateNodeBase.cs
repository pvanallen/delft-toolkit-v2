﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace DelftToolkit {
    [CreateNodeMenu("")]
	public class StateNodeBase : Node {

		[Input] public Empty enter;
		[Output] public Empty exit;

		public void MoveNext() {
			StateGraph fmGraph = graph as StateGraph;

			if (fmGraph.current != this) {
				Debug.LogWarning("Node isn't active");
				return;
			}

			NodePort exitPort = GetOutputPort("exit");

			if (!exitPort.IsConnected) {
				Debug.LogWarning("Node isn't connected");
				return;
			}

			StateNodeBase node = exitPort.Connection.node as StateNodeBase;
			for (int i = 0; i < exitPort.ConnectionCount; i++) {
                StateNodeBase nextNode = exitPort.GetConnection(i).node as StateNodeBase;
				if (nextNode != null) nextNode.OnEnter();
			}
		}


		public virtual void OnEnter() {
			StateGraph fmGraph = graph as StateGraph;
			fmGraph.current = this;
			Debug.LogWarning("New Node starting");
            //MyNodeEditor.NodeEditorWindow.current.Repaint();
		}

        public virtual void OnExit() {
            MoveNext();
        }

		public IEnumerator Finish(float delay ) {
			yield return new WaitForSeconds(delay);
			OnExit ();
		}

		[Serializable]
		public class Empty { }
	}
}