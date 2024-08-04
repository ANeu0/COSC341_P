using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    internal class Data
    {
        string Name;
        string Added;
        string TriggeredAt;
        string DeltaTime;
        string PositionX;
        string PositionY;
        string PositionZ;
        string Technique;
        string Width;
        string Height;
        string Depth;
        string Amplitude;
        string Correct;
        public Data() { }
        public Data(string name,
            DateTime lastTimeTriggered,
            DateTime timeTriggered,
            string technique,
            string positionX,
            string positionY,
            string positionZ,
            string width,
            string height,
            string depth,
            string amplitude)
        {
            Name = name;
            Added = DateTime.Now.ToString();
            TriggeredAt = timeTriggered.ToString();
            TimeSpan deltaTime = timeTriggered - lastTimeTriggered;
            UnityEngine.Debug.Log(deltaTime.TotalSeconds.ToString());
            DeltaTime = deltaTime.TotalSeconds.ToString();
            PositionX = positionX;
            PositionY = positionY;
            PositionZ = positionZ;
            Width = width;
            Height = height;
            Depth = depth;
            Technique = technique;
            Width = width;
            Amplitude = amplitude;
        }
    }
}