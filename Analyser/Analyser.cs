/* (c) 2004 Johan Berntsson, j.berntsson@qut.edu.au
 * Queensland University of Technology, Brisbane, Australia
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 */
using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Collections;
using System.Collections.Specialized;
using System.Windows.Forms;

using dotGALib; // for GenericList

namespace Analyser {
  /// <summary>
  /// This class loads a data dump file from the supervisor
  /// and plot data in many ways from it.
  /// </summary>

  public class Analyser {
    public enum GraphType { PNG, CGM, TEX };

    public Analyser() {
      showMeanFitness=false;
      showMaxFitness=false;
      showMinFitness=false;
      showSpeed=false;
      showAdaptationEvents=false;
      showMigrationEvents=false;
      showGlobalOptima=true;
      showClusters=false;
      showConnectivity=false;
      showAllIslands=true;
      useCustomized=false;
      showStddev=false;
      trendDataPath="gnu_trend.csv";
      speedDataPath="gnu_speed.csv";
      customizeDataPath="gnu_custom.txt";
      allRunDataPath="gnu_allruns";
    }

    #region Properties
    public string FilePath {
      get { return filePath; }
    }

    public string Summary {
      get { 
        if(showAllRuns) return summary;
        return currentRun.summary; 
      }
    }

    public bool ShowAdaptationEvents {
      get { return showAdaptationEvents; }
      set { showAdaptationEvents=value; }
    }

    public bool ShowMigrationEvents {
      get { return showMigrationEvents; }
      set { showMigrationEvents=value; }
    }

    public bool ShowGlobalOptima {
      get { return showGlobalOptima; }
      set { showGlobalOptima=value; }
    }

    public bool ShowStddev {
      get { return showStddev; }
      set { showStddev=value; }
    }

    public bool ShowClusters {
      get { return showClusters; }
      set { showClusters=value; }
    }

    public bool ShowConnectivity {
      get { return showConnectivity; }
      set { showConnectivity=value; }
    }

    public bool ShowMeanFitness {
      get { return showMeanFitness; }
      set { showMeanFitness=value; }
    }

    public bool ShowMaxFitness {
      get { return showMaxFitness; }
      set { showMaxFitness=value; }
    }

    public bool ShowMinFitness {
      get { return showMinFitness; }
      set { showMinFitness=value; }
    }

    public bool ShowSpeed {
      get { return showSpeed; }
      set { showSpeed=value; }
    }
    
    public int NumIslands {
      get { if(currentRun==null) return 0; return currentRun.numIslands; }
    }

    public int NumRuns {
      get { return run.Count; }
    }

    public bool AllRuns {
      get { return showAllRuns; }
      set { showAllRuns=value; }
    }

    public bool ShowRun(int index) {
      return (currentRun==run[index]);
    }

    public void SetCurrentRun(int index) {
      if(currentRun!=run[index]) {
        currentRun=run[index];

        // Create data files
        using(StreamWriter sw=new StreamWriter(trendDataPath)) {
          sw.WriteLine(currentRun.trendData);
          sw.Close();
        }

        using(StreamWriter sw=new StreamWriter(speedDataPath)) {
          sw.WriteLine(currentRun.speedData);
          sw.Close();
        }
      }
    }

    public bool ShowAllIslands {
      get { return showAllIslands; }
      set { showAllIslands=value; }
    }

    public bool ShowIsland(int index) {
      return (bool) showIsland[index];
    }

    public void ShowIsland(int index, bool state) {
      if(index<showIsland.Count) {
        showIsland[index]=state;
      } else {
        while(showIsland.Count<=index)  {
          showIsland.Add(state);
        }
      }
    }

    public void CustomizeGraph(string commands) {
      useCustomized=true;
      using(StreamWriter sw=new StreamWriter(customizeDataPath)) {
        sw.WriteLine(commands);
        sw.Close();
      }
    }
    #endregion

    #region Parse PGA data
    public void Read(string path, int index, ProgressBar progress) {
      // Parse the PGA data file and create gnuplot data files
      filePath=path;
      int readBytes=0;
      FileInfo f=new FileInfo(filePath);
      progress.Minimum=0;
      progress.Maximum=(int) f.Length;

      while(files.Count<=index) files.Add("");
      string ext=f.Extension;
      if(ext.Length>0) {
        files[index]=f.Name.Substring(0, f.Name.Length-ext.Length);
      } else {
        files[index]=f.Name;
      }
      numFiles=index+1;

      run.Clear();
      currentRun=new RunData();
      run.Add(currentRun);

      string line;
      string comment="";
      using(StreamReader file=new StreamReader(filePath)) {
        while((line=file.ReadLine())!=null) {
          Console.WriteLine(line);
          ParseLine(line, ref comment);
          readBytes+=line.Length;
          progress.Value=readBytes;
        }
        file.Close();
      }

      CreateTrendFileContent();

      double meanOptima, stddevOptima, bestOptima, meanFitness, stddevFitness;
      AllRunData(index, out meanOptima, out stddevOptima, out bestOptima, out meanFitness, out stddevFitness);


      // Make final adjustments of data for each run.
      summary="File: "+filePath+"\r\nComment: "+comment;
      int i=0;
      foreach(RunData r in run) {
        r.speedData.Replace("@", "\r\n");
        r.summary=summary+"\r\nOptima: "+r.currentTrend.optimalValue+"\r\nRun: "+(i++)+"\r\n\r\nEvents:\r\n";
        for(int j=0; j<r.migrationEvent.Count; j++) {
          r.summary+=r.migrationEvent[j].time+"\t"+r.migrationEvent[j].connectivity+"\t";
          if(r.migrationEvent[j].topology.Length==0) {
            r.summary+="FullyConnected";
          } else {
            r.summary+=r.migrationEvent[j].topology;
          }
          r.summary+="\r\n";
        }
      }
      summary+="\r\nOptima: "+bestOptima+" "+meanOptima+" "+stddevOptima;
      summary+="\r\nFitness: "+meanFitness+" "+stddevFitness;

      summary+="\r\n"+bestOptima+","+meanOptima+","+stddevOptima+",";
      summary+=meanFitness+","+stddevFitness+",";

      Console.WriteLine("{0},{1:g5},{2:g5},{3:g5},{4:g5},{5:g5}", filePath.Substring(filePath.Length-7), bestOptima, meanOptima,
        stddevOptima, meanFitness, stddevFitness);

      currentRun=null;
    }

    private void ParseLine(string line, ref string comment) {
      double speed;
      int island, generations, numClusters;
      double connectivity;

      string delimStr = " ,\t";
      char[] delimiter = delimStr.ToCharArray();
      string[] tokens=line.Split(delimiter);

      int time=int.Parse(tokens[0]);

      switch(tokens[1]) {
        case "Comment":
          comment=line.Substring(10);
          break;
        case "Run":
          // Start of data from a new run
          if(int.Parse(tokens[2])>0) {
            currentRun=new RunData();
            run.Add(currentRun);
          }
          break;
        case "Start":
          currentRun.numIslands=int.Parse(tokens[2]);
          currentRun.lastGenUpdateTime=new double[currentRun.numIslands];
          currentRun.lastGenNumber=new int[currentRun.numIslands];

          while(showIsland.Count<currentRun.numIslands) {
            showIsland.Add(false);
          }
          break;
        case "End":
          break;
        case "AdaptEventTimer":
          numClusters=int.Parse(tokens[2]);
          currentRun.adaptationEvent.Add(new AdaptationEventData(time, numClusters));

          UpdateCurrentTrend(time);
          currentRun.currentTrend.numClusters=numClusters;

          currentRun.speedData.Append(time+",");
          for(int i=0; i<currentRun.numIslands;i++) currentRun.speedData.Append(',');
          currentRun.speedData.Append(numClusters+",@");

          break;
        case "Mutation":
          currentRun.mutationEvent.Add(new MutationEventData(time, double.Parse(tokens[2])));
          break;
        case "MigrationPolicy":
          numClusters=int.Parse(tokens[2]);
          connectivity=double.Parse(tokens[3]);
          string topology="";
          int idx=line.IndexOf(':');
          if(idx>0) topology=line.Substring(idx+3);
          if(idx==-1) topology=tokens[4];
          currentRun.migrationEvent.Add(new MigrationEventData(time, numClusters, connectivity, topology));

          UpdateCurrentTrend(time);
          currentRun.currentTrend.connectivity=connectivity;

          currentRun.speedData.Append(time+",");
          for(int i=0; i<currentRun.numIslands;i++) currentRun.speedData.Append(',');
          currentRun.speedData.Append(","+connectivity+"@");

          break;
        case "SupervisorOptima":
          UpdateCurrentTrend(time);
          currentRun.currentTrend.optimalValue=double.Parse(tokens[2]);
          break;
        case "Migrant":
          island=int.Parse(tokens[2]);
          generations=int.Parse(tokens[3]);

          UpdateCurrentTrend(time);
          currentRun.currentTrend.mean[island]=double.Parse(tokens[4]);
          currentRun.currentTrend.stddev[island]=double.Parse(tokens[5]);
          currentRun.currentTrend.max[island]=double.Parse(tokens[6]);
          currentRun.currentTrend.min[island]=double.Parse(tokens[7]);

          // Speed data
          currentRun.speedData.Append(time+",");
          for(int i=0; i<currentRun.numIslands;i++) {
            if(i==island) {
              speed=(generations-currentRun.lastGenNumber[i])/(time-currentRun.lastGenUpdateTime[i]);
              currentRun.lastGenUpdateTime[i]=time;
              currentRun.lastGenNumber[i]=generations;
              currentRun.speedData.Append(speed);
            }
            currentRun.speedData.Append(',');
          }
          currentRun.speedData.Append(",@");
          break;
        default:
          throw new NotImplementedException(tokens[1]);
      }
    }

    private void UpdateCurrentTrend(int time) {
      while(currentRun.currentTrend==null || currentRun.currentTrend.time<time) {
        currentRun.currentTrend=new TrendData(currentRun.numIslands, currentRun.currentTrend);
        currentRun.trend.Add(currentRun.currentTrend);
      }
    }

    private string AllRunDataPath(int index) {
      string x=allRunDataPath;
      if(index>-1) x+=index.ToString();
      return x+".csv";
    }

    private void AllRunData(int index, out double meanOptima, out double stddevOptima, out double bestOptima, out double meanFitness, out double stddevFitness) {
      // Find out which run has the least data points
      int numTrends=run[0].trend.Count;
      for(int i=1; i<run.Count; i++) {
        if(numTrends>run[i].trend.Count) numTrends=run[i].trend.Count;
      }

      double mean1=0, stddev1=0, mean2=0, stddev2=0, mean3, stddev3;
      double mean4, stddev4, mean5, stddev5, mean6, stddev6;

      StringBuilder allRunData=new StringBuilder();
      for(int i=0; i<numTrends; i++) {
        AllRunStatistics(0, i, out mean1, out stddev1); // optimal
        AllRunIslandStatistics(0, i, out mean2, out stddev2); // mean
        AllRunIslandStatistics(1, i, out mean3, out stddev3); // max
        AllRunIslandStatistics(2, i, out mean4, out stddev4); // min
        AllRunStatistics(1, i, out mean5, out stddev5); //numClusters
        AllRunStatistics(2, i, out mean6, out stddev6); //connectivity
        allRunData.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}\r\n",
          run[0].trend[i].time, mean1, stddev1, mean2, stddev2, mean3,
          stddev3, mean4, stddev4, mean5, stddev5, mean6, stddev6);
      }

      // Create data files
      using(StreamWriter sw=new StreamWriter(AllRunDataPath(index))) {
        sw.WriteLine(allRunData);
        sw.Close();
      }

      meanFitness=mean2; // mean of fitness for last trend
      stddevFitness=stddev2; // stddev of fitness for last trend

      meanOptima=mean1; // mean of optimalValue for last trend
      stddevOptima=stddev1; // stddev of optimalValue for last trend

      bestOptima=meanOptima; // best optimalValue in last trend
      for(int j=0; j<run.Count; j++) {
        double d=run[j].trend[numTrends-1].optimalValue;
        if(bestOptima>d) bestOptima=d;
      }
    }

    private void AllRunStatistics(int t, int i, out double mean, out double stddev) {
      mean=0;
      int numRuns=run.Count;
      for(int j=0; j<numRuns; j++) {
        if(t==0) mean+=run[j].trend[i].optimalValue;
        if(t==1) mean+=run[j].trend[i].numClusters;
        if(t==2) mean+=run[j].trend[i].connectivity;
      }
      mean/=numRuns;

      stddev=0;
      for(int j=0; j<numRuns; j++) {
        double d=0;
        if(t==0) d=run[j].trend[i].optimalValue-mean;
        if(t==1) d=run[j].trend[i].numClusters-mean;
        if(t==2) d=run[j].trend[i].connectivity-mean;
        stddev+=d*d;
      }
      if(numRuns>1) stddev/=(numRuns-1);
      stddev=Math.Sqrt(stddev);
    }

    private void AllRunIslandStatistics(int type, int i, out double mean, out double stddev) {
      int numRuns=run.Count;
      int numIslands=run[0].numIslands;
      double[] runMean=new double[run.Count];

      // First calculate mean of all islands for each run
      for(int j=0; j<numRuns; j++) {
        runMean[j]=0;
        for(int k=0; k<numIslands; k++) {
          if(type==0) runMean[j]+=run[j].trend[i].mean[k];
          if(type==1) runMean[j]+=run[j].trend[i].max[k];
          if(type==2) runMean[j]+=run[j].trend[i].min[k];
        }
        runMean[j]/=numIslands;
        if(runMean[j].CompareTo(double.NaN)==0) {
          mean=double.NaN;
          stddev=double.NaN;
          return;
        }
      }

      // Calculate mean of all run
      mean=0;
      for(int j=0; j<numRuns; j++) {
        mean+=runMean[j];
      }
      mean/=numRuns;

      stddev=0;
      for(int j=0; j<numRuns; j++) {
        double d=runMean[j]-mean;
        stddev+=d*d;
      }
      if(numRuns>1) stddev/=(numRuns-1);
      stddev=Math.Sqrt(stddev);
    }

    private void CreateTrendFileContent() {
      foreach(RunData r in run) {
        r.trendData=new StringBuilder();
        for(int i=0; i<r.trend.Count; i++) {
          TrendData t=r.trend[i];
          r.trendData.Append(t.time+",");
          for(int j=0; j<r.numIslands; j++) {
            r.trendData.AppendFormat("{0},{1},{2},{3},", t.mean[j], t.stddev[j], t.max[j], t.min[j]);
          }
          r.trendData.AppendFormat("{0}\r\n", t.optimalValue);
        }
      }
    }
    #endregion

    #region Create graph
    public void MakeGraphFile(string path, GraphType type) {
      // Create gnuplot script
      string script="set datafile separator ','@";
      script+="set key below@";

      if(type==GraphType.PNG) {
        script+="set terminal png@";
      } else if(type==GraphType.TEX) {
        script+="set terminal tex@";
        //script+="set size 0.5, 0.5@";
      } else {
        script+="set terminal cgm@";
      }

      if(useCustomized) script+="load '"+customizeDataPath+"@";

      plotCommand="plot ";

      script+="set xlabel 'Time (ms)'@";
      script+="set ylabel 'Fitness'@";

      string trends;
      if(showAllRuns || numFiles>1) {
        trends=MakeAllRunsGraph();
      } else {
        trends=MakeRunGraph();
      }

      // Create output
      if(trends.Equals("plot @") || trends.Length==0) {
        script+="set output '"+path+"'@";
      } else {
        script+=trends;
        script+="set output '"+path+"'@";
        script+="replot@";
      }
      script+="set terminal windows@";

      string scriptPath="gnu.txt";
      using(StreamWriter sw=new StreamWriter(scriptPath)) {
        sw.WriteLine(script.Replace("@", "\r\n"));
        sw.Close();
      }

      // Call external program gnuplot to generate trend picture
      using(Process gnuplot=System.Diagnostics.Process.Start(Preferences.gnuplotPath, scriptPath)) {
        gnuplot.WaitForExit();
        if(gnuplot.ExitCode!=0) throw new ApplicationException("Gnuplot script error");
      }
    }

    private string MakeAllRunsGraph() {
      string command="";
      string id;
      for(int i=0; i<numFiles; i++) {
        string script="";
        if(numFiles>1) id=files[i]+", "; else id="";
        if(showGlobalOptima) {
          if(script.Length>0) script+=", "; else script+=plotCommand;
          script+="'"+AllRunDataPath(i);
          if(showStddev) {
            script+="' using 1:2:3 t '"+id+"Optima' with errorlines";
          } else {
            script+="' using 1:2 t '"+id+"Optima' with lines";
          }
        }
        if(showClusters) {
          if(script.Length>0) script+=", "; else script+=plotCommand;
          script+="'"+AllRunDataPath(i);
          script+="' using 1:10:11 t '"+id+"Clusters' with errorlines";
        }
        if(showMeanFitness) {
          if(script.Length>0) script+=", "; else script+=plotCommand;
          script+="'"+AllRunDataPath(i);
          if(showStddev) {
            script+="' using 1:4:5 t '"+id+"Mean fitness' with errorlines";
          } else {
            script+="' using 1:4 t '"+id+"Mean fitness' with lines";
          }
        }
        if(showMaxFitness) {
          if(script.Length>0) script+=", "; else script+=plotCommand;
          script+="'"+AllRunDataPath(i);
          script+="' using 1:6:7 t '"+id+"Max fitness' with errorlines";
        }
        if(showMinFitness) {
          if(script.Length>0) script+=", "; else script+=plotCommand;
          script+="'"+AllRunDataPath(i);
          script+="' using 1:8:9 t '"+id+"Min fitness' with errorlines";
        }
        if(showConnectivity) { // must be last!
          if(script.Length>0) script+=", "; else script+=plotCommand;
          script+="'"+AllRunDataPath(i);
          if(showStddev) {
            script+="' using 1:12:13 t '"+id+"Connectivity' with errorlines axis x1y2";
          } else {
            script+="' using 1:12 t '"+id+"Connectivity' with lines axis x1y2";
          }
          script+="@set ytics nomirror@";
          script+="set y2range [0:1]@";
          script+="set y2tics 0, 0.1@";
          script+="set y2label 'Connectivity'@";
        }
        command+=script+"@";
        plotCommand="replot ";
      }

      return command;
    }

    private string MakeRunGraph() {
      string script="";
      if(showMeanFitness) script+=MakeMeanFitnessGraph();
      if(showMaxFitness) script+=MakeMaxFitnessGraph();
      if(showMinFitness) script+=MakeMinFitnessGraph();
      if(showGlobalOptima) script+=MakeOptimaGraph();
      if(showSpeed) script+=MakeSpeedGraph();
      if(showMigrationEvents) script+=MakeMigrationLines();
      if(showAdaptationEvents) script+=MakeAdaptationLines();
      if(showClusters) script+=MakeClustersGraph();
      if(showConnectivity) script+=MakeConnectivityGraph();
      return script;
    }

    private string MakeClustersGraph() {
      string script=plotCommand;
      script+="'"+speedDataPath+"' using 1:"+(currentRun.numIslands+2);
      script+=" t 'Number of clusters' with lines";
      plotCommand="replot ";
      return script+"@";
    }

    private string MakeConnectivityGraph() {
      string script=plotCommand;
      script+="'"+speedDataPath+"' using 1:"+(currentRun.numIslands+3);
      script+=" t 'Connectivity' with lines axis x1y2";
      script+="@set ytics nomirror@";
      script+="set y2range [0:1]@";
      script+="set y2tics 0, 0.1@";
      script+="set y2label 'Connectivity'@";
      plotCommand="replot ";
      return script+"@";
    }

    private string MakeMigrationLines() {
      string script="";
      for(int i=0; i<currentRun.migrationEvent.Count; i++) {
        int time=currentRun.migrationEvent[i].time;
        script+=String.Format("set arrow from {0}, graph 0 to {1}, graph 1 nohead@", time, time);
      }
      return script;
    }

    private string MakeAdaptationLines() {
      string script="";
      for(int i=0; i<currentRun.adaptationEvent.Count; i++) {
        int time=currentRun.adaptationEvent[i].time;
        script+=String.Format("set arrow from {0}, graph 0 to {1}, graph 1 nohead@", time, time);
      }
      return script;
    }

    private string MakeSpeedGraph() {
      int cnt=0;
      string script=plotCommand;
      for(int i=0; i<currentRun.numIslands; i++) {
        if((bool) showIsland[i] || showAllIslands) {
          if(cnt>0) script+=", ";
          script+="'"+speedDataPath+"' using 1:"+(i+2);
          script+=" t '"+string.Format("Island {0}", i)+"' with lines";
          ++cnt;
        }
      }
      plotCommand="replot ";
      return script+"@";
    }

    private string MakeMeanFitnessGraph() {
      int cnt=0;
      string script=plotCommand;
      for(int i=0; i<currentRun.numIslands; i++) {
        if((bool) showIsland[i] || showAllIslands) {
          if(cnt>0) script+=", ";
          script+="'"+trendDataPath+"' using 1:"+(i*4+2)+":"+(i*4+3);
          script+=" t '"+string.Format("Mean fitness, Island {0}", i)+"' with errorlines";
          cnt+=4;
        }
      }

      plotCommand="replot ";
      return script+"@";
    }

    private string MakeOptimaGraph() {
      string script=plotCommand;
      script+="'"+trendDataPath+"' using 1:"+(currentRun.numIslands*4+2);
      script+=" t 'Global optima' with lines";
      plotCommand="replot ";
      return script+"@";
    }

    private string MakeMaxFitnessGraph() {
      int cnt=0;
      string script=plotCommand;
      for(int i=0; i<currentRun.numIslands; i++) {
        if((bool) showIsland[i] || showAllIslands) {
          if(cnt>0) script+=", ";
          script+="'"+trendDataPath+"' using 1:"+(i*4+4);
          script+=" t '"+string.Format("Max fitness, Island {0}", i)+"' with lines";
          cnt+=4;
        }
      }

      plotCommand="replot ";
      return script+"@";
    }

    private string MakeMinFitnessGraph() {
      int cnt=0;
      string script=plotCommand;
      for(int i=0; i<currentRun.numIslands; i++) {
        if((bool) showIsland[i] || showAllIslands) {
          if(cnt>0) script+=", ";
          script+="'"+trendDataPath+"' using 1:"+(i*4+5);
          script+=" t '"+string.Format("Min fitness, Island {0}", i)+"' with lines";
          cnt+=4;
        }
      }

      plotCommand="replot ";
      return script+"@";
    }

    #endregion

    private string filePath;
    private string trendDataPath;
    private string speedDataPath;
    private string customizeDataPath;
    private string allRunDataPath;

    private class TrendData {
      public TrendData(int numIslands, TrendData lastTrend) {
        mean=new double[numIslands];
        stddev=new double[numIslands];
        max=new double[numIslands];
        min=new double[numIslands];
        if(lastTrend==null) {
          time=0;
          numClusters=0;
          connectivity=double.NaN;
          optimalValue=double.NaN;
          for(int i=0; i<numIslands; i++) {
            mean[i]=double.NaN;
            stddev[i]=double.NaN;
            max[i]=double.NaN;
            min[i]=double.NaN;
          }
        } else {
          time=lastTrend.time+1;
          numClusters=lastTrend.numClusters;
          connectivity=lastTrend.connectivity;
          optimalValue=lastTrend.optimalValue;
          for(int i=0; i<numIslands; i++) {
            mean[i]=lastTrend.mean[i];
            stddev[i]=0;
            max[i]=lastTrend.max[i];
            min[i]=lastTrend.min[i];
          }
        }
      }

      public int time;
      public double[] mean;
      public double[] stddev;
      public double[] max;
      public double[] min;
      public int numClusters;
      public double connectivity;
      public double optimalValue;
    }

    private class MutationEventData {
      public MutationEventData(int time, double mutationRate) {
        this.time=time;
        this.mutationRate=mutationRate;
      }
      public int time;
      public double mutationRate;
    }

    private class AdaptationEventData {
      public AdaptationEventData(int time, int numClusters) {
        this.time=time;
        this.numClusters=numClusters;
      }
      public int time;
      public int numClusters;
    }

    private class MigrationEventData {
      public MigrationEventData(int time, int numClusters, double connectivity, string topology) {
        this.time=time;
        this.numClusters=numClusters;
        this.connectivity=connectivity;
        this.topology=topology;
      }
      public int time;
      public int numClusters;
      public double connectivity;
      public string topology;
    }

    private class TrendList: GenericList {
      public TrendData this[int index] {
        get { return (TrendData) data[index]; }
        set { data[index]=value; }
      }
    }

    private class AdaptationEventList: GenericList {
      public AdaptationEventData this[int index] {
        get { return (AdaptationEventData) data[index]; }
        set { data[index]=value; }
      }
    }

    private class MutationEventList: GenericList {
      public MutationEventData this[int index] {
        get { return (MutationEventData) data[index]; }
        set { data[index]=value; }
      }
    }

    private class MigrationEventList: GenericList {
      public MigrationEventData this[int index] {
        get { return (MigrationEventData) data[index]; }
        set { data[index]=value; }
      }
    }

    private class RunData {
      public TrendData currentTrend=null;
      public TrendList trend=new TrendList();

      public MigrationEventList migrationEvent=new MigrationEventList();
      public MutationEventList mutationEvent=new MutationEventList();
      public AdaptationEventList adaptationEvent=new AdaptationEventList();

      public int numIslands;//=10; // JB, temporary fix
      public string summary;

      public int[] lastGenNumber;
      public double[] lastGenUpdateTime;

      public StringBuilder trendData=new StringBuilder();
      public StringBuilder speedData=new StringBuilder();
    }

    private class RunList: GenericList {
      public RunData this[int index] {
        get { return (RunData) data[index]; }
        set { data[index]=value; }
      }
    }

    private RunData currentRun;
    private RunList run=new RunList();
    private string summary;
    private string plotCommand;

    public ArrayList showIsland=new ArrayList();

    private bool showMeanFitness;
    private bool showMaxFitness;
    private bool showMinFitness;
    private bool showSpeed;
    private bool showAdaptationEvents;
    private bool showMigrationEvents;
    private bool showAllIslands;
    private bool showAllRuns;
    private bool showGlobalOptima;
    private bool showClusters;
    private bool showConnectivity;
    private bool useCustomized;
    private bool showStddev;
    private int numFiles;

    StringCollection files=new StringCollection();
  }
}
