//----------------------------------------------
//            Shader Weaver
//      Copyright© 2017 Jackie Lo
//----------------------------------------------
namespace ShaderWeaver
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using ShaderWeaver;
	using System.Threading;
	using UnityEditor;

	public class IntRect
	{
		public int xMin = int.MaxValue;
		public int xMax = int.MinValue;
		public int yMin = int.MaxValue;
		public int yMax = int.MinValue;
	}
	public class tData
	{
		public int x1;
		public int x2;
		public int y1;
		public int y2;

		public System.Action<int,int> act;
	}

	public class TexThread {
		protected bool displayProgress;
		protected int counter;

		protected float totaltimer;
		protected float timer;
		protected Dictionary<string,float> timerDic = new Dictionary<string, float>();

		protected int threadUnit = 6;
		protected int xUnit;
		protected int yUnit;
		protected IntRect rect = new IntRect();

		public TexThread()
		{
			totaltimer = Time.realtimeSinceStartup;
			timer = Time.realtimeSinceStartup;
		}

		protected void Process()
		{
			CalRect ();
			xUnit = (rect.xMax - rect.xMin) / threadUnit;
			yUnit = (rect.yMax - rect.yMin) / threadUnit;
			Process (ThreadMission_Pixel,MissionEnd);
		}

		protected void Process(System.Action<int,int> act,System.Action end)
		{
			counter = 0;
			for (int i = 0; i < threadUnit; i++) {
				for (int j = 0; j < threadUnit; j++) {
					tData u = new tData ();
					u.x1 = rect.xMin + xUnit * i;
					u.x2 = u.x1 + xUnit;
					u.y1 = rect.yMin + yUnit * j;
					u.y2 = u.y1 + yUnit;
					u.act = act;
					Thread thread = new Thread(ThreadMission);
					thread.Start(u);
				}
			}
			EditorCoroutineRunner.StartEditorCoroutine (End (end));
		}

		protected virtual void CalRect()
		{

		}
		private void ThreadMission(object obj)
		{
			try
			{
				tData u = (tData)obj;
				for(int i=u.x1;i<u.x2;i++)
				{
					for(int j=u.y1;j<u.y2;j++)
					{
						if(u.act!=null)
							u.act(i,j);
					}
				}
				counter++;
				//Debug.Log (string.Format ("x1:{0} x2:{1} y1:{2} y2:{3} counter:{4}",u.x1, u.x2, u.y1, u.y2, counter));
			}
			catch(System.Exception e) {
				Debug.LogError ("Thread exception:" + e.ToString());
			}
		}
		protected virtual void ThreadMission_Pixel(int i,int j)
		{

		}
		protected IEnumerator End(System.Action end)
		{
			while (counter<(threadUnit*threadUnit)) {
				if (displayProgress) {
					float y = threadUnit * threadUnit;
					float progress = (float)counter / y;
					EditorUtility.DisplayProgressBar ("Progress", "Processing texture...", progress);
				}
				yield return null;
			}
			if(end!=null)
				end();
			EditorUtility.ClearProgressBar ();
		}
		protected virtual void MissionEnd()
		{
			//Debug.Log ("End " + (Time.realtimeSinceStartup - totaltimer));
		}

		protected void TimerCost(string str)
		{
			//Debug.Log (str + ": " + (Time.realtimeSinceStartup - timer));
			timer = Time.realtimeSinceStartup;
		}

		protected void TimerRec(string str)
		{
			if (!timerDic.ContainsKey (str))
				timerDic.Add (str, 0);

			timerDic [str] += Time.realtimeSinceStartup - timer;
			timer = Time.realtimeSinceStartup;
		}

		protected void TimerSum(string str)
		{
			//Debug.Log (str + ": " + timerDic [str]);
		}
	}
}