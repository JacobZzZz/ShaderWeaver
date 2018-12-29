//----------------------------------------------
//            Shader Weaver
//      Copyright© 2017 Jackie Lo
//----------------------------------------------
namespace ShaderWeaver
{
	using UnityEngine;
	using System.Collections;
	using UnityEditor;

	[System.Serializable]
	public class SWNodeMixer :SWNodeEffector {
		public readonly int minPortCount = 1;
		public readonly int maxPortCount = 6;

		public override void Init (SWDataNode _data, SWWindowMain _window)
		{
			childPortSingleConnection = true;
			nodeWidth = NodeBigWidth;
			nodeHeight = 80;
			styleID = 1;
			base.Init (_data, _window);
			data.outputType.Add (SWDataType._Color);
			data.outputType.Add (SWDataType._UV);
			data.outputType.Add (SWDataType._Alpha);
			data.inputType.Add (SWDataType._Color);
			data.inputType.Add (SWDataType._UV);
			data.inputType.Add (SWDataType._Alpha);
			UpdatePort ();

			foreach (var item in data.gradients) {
				item.UpdateTex ();
			}
		}

		public override bool PortMatch (int port, SWNodeBase child, int childPort)
		{
			return base.PortMatch (port, child, childPort);
		}

		protected override void DrawNodeWindow (int id)
		{
			base.DrawNodeWindow (id);
			DrawPortAdd();
			DrawPortDelete ();
			DrawNodeWindowEnd ();
		}
		void DrawPortDelete()
		{
			int count = data.childPortNumber-1;
			float pw = portHeight;
			float ph = portWidth+5;

			int toDeletePort = -1;
			for (int i = 0; i < count; i++) {
				Rect rectDelete = new Rect (
					data.rect.width * 0.5f - pw * 0.5f  - (float)(count-1)/2f * (portSpacing+pw) + (portSpacing+pw)*i,
					data.rect.height - ph - 4, 
					pw, 
					ph
				);
				if (GUI.Button (rectDelete,"-",SWEditorUI.MainSkin.button)) {
					toDeletePort = i+1;
				}
			}
			if (toDeletePort > 0) {
				DeletePort (toDeletePort);
			}
		}

		void DeletePort(int toDeletePort)
		{
			data.gradients.RemoveAt (toDeletePort-1);
			data.childPortNumber--;
			for (int i = data.children.Count-1; i >= 0; i--) {
				if (data.childrenPort [i] == toDeletePort) {
					data.childrenPort.RemoveAt (i);
					data.children.RemoveAt (i);
				}
				else if (data.childrenPort [i] > toDeletePort) {
					data.childrenPort [i]--;
				}
			}
		}

		void AddPort()
		{
			data.childPortNumber++;
			data.childPortNumber = Mathf.Clamp (data.childPortNumber, minPortCount, maxPortCount);
		}



		void DrawPortAdd()
		{
			if (data.childPortNumber < maxPortCount) {
				GUILayout.Space (gap + 2);
				if (GUILayout.Button ("Add", SWEditorUI.MainSkin.button)) {
					AddPort ();
				}
			}
		}

		protected override void SetRectsLeft (System.Collections.Generic.List<Rect> rects, int count = 1)
		{
			base.SetRectsLeft (rects, count);
			rects.Clear ();
			for (int i = 0; i < count; i++) {
				if (i == 0) {
					rects.Add (CalRectVertical (data.rect.x - portWidth, 0, 1));
				} else {
					rects.Add (CalRectHorizontal (data.rect.y +data.rect.height, i-1, count-1));
				}
			}
		}

		public override void InitLayout ()
		{
			base.InitLayout ();
			rectArea = new Rect (gap, headerHeight + gap, contentWidth, 
				nodeHeight - headerHeight - gap*2 - (rectBotButton.height+gap) - 20);
		}


		protected override void DrawLeftRect (int id, Rect rect)
		{
			if (id == 0) {
				base.DrawLeftRect (id, rect);
			} else {
				UpdatePort ();
				SWEditorTools.DrawTiledTexture (rect, SWEditorTools.backdropTexture);
				GUI.DrawTexture (rect, data.gradients [id-1].Tex);
				if (window.InMapClick(0) && rect.Contains (window.mousePosLast))
					SWWindowMixerEditor.Show (data.gradients [id-1]);
			}
		}

		public void UpdatePort()
		{
			int portNumForMix = data.childPortNumber - 1;
			if (portNumForMix < data.gradients.Count) {
				for (int i = data.gradients.Count-1; i>=0; i--) {
					if (i >= portNumForMix) {
						data.gradients.RemoveAt (i);
					}
				}
			}
			if (portNumForMix > data.gradients.Count) {
				
				while (data.gradients.Count < portNumForMix) {
					var newGradient = new SWGradient ();
					newGradient.UpdateTex ();
					data.gradients.Add (newGradient);
				}
			
			}

			var tempNodeAll = window.NodeAll ();
			for (int i = data.children.Count-1; i>=0; i--) {
				if (data.childrenPort [i] >= data.childPortNumber) {
					string child = data.children [i];
					RemoveConnection(this,tempNodeAll[child]);
				}
			}
		}


		public static int Gradient_MaxFrameCount()
		{
			int maxCount = 0;
			foreach (var node in SWWindowMain.Instance.nodes) {
				if (node.data.type == SWNodeType.mixer) {
					foreach (var gradient in node.data.gradients) {
						if (gradient.frames.Count > maxCount)
							maxCount = gradient.frames.Count;
					}
				}
			}
			return maxCount;
		}
	}
}
