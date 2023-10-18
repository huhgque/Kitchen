using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProgressBarDisplay
{
    
    public event EventHandler<ProgressBarArgs> OnProgressBarChange;
    public class ProgressBarArgs:EventArgs{
        public float progress;
    }

}
