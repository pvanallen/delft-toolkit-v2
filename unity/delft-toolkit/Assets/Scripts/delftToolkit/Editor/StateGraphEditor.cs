﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using XNode;
using XNodeEditor;

namespace DelftToolkit {
	[CustomNodeGraphEditor(typeof(StateGraph))]
	public class StateGraphEditor : NodeGraphEditor {

		/// <summary> 
		/// Overriding GetNodePath lets you control if and how nodes are categorized.
		/// In this example we are sorting out all node types that are not in the XNode.Examples namespace.
		/// </summary>
		public override string GetNodeMenuName(System.Type type) {
			if (type.Namespace == "DelftToolkit") {
				return base.GetNodeMenuName(type).Replace("Delft Toolkit/", "");
			} else return null;
		}

        [InitializeOnLoadMethod]
        private static void initEvents() {
            Actions.DingEvent -= SetAction;
            Actions.DingEvent += SetAction;
        }

        public static void SetAction(AiGlobals.Devices aDevice, Action anAction) {
            //Debug.LogWarning("Repainting");
            NodeEditorWindow.current.Repaint();
        }
	}
}