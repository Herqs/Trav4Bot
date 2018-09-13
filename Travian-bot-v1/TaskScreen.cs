using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using System.Threading;
using SysTimer = System.Windows.Forms.Timer;
namespace Travian_bot_v1
{
    public partial class TaskScreen : Form
    {
        TimeSpan SysTime;
        private bool[,] TaskBusyBool;
        private bool DoingStuff = false;
        private Thread TasksWorkerThread;
        SysTimer timer, ResourceRefreshTimer, TasksTimer, TasklistRefreshTimer;
        int counter, TasksInterval, forcerefreshtimer = 300, forcerefreshduration;
        TravianBrowser tb;
        List<TravianDoingTaskBuild> DoingTaskBuildList;
        List<TravianTaskBuild> TaskBuildBuildList;
        List<TravianVillage> VillageList;
        TravianVillage CurrentVillage;

        public TaskScreen(TravianBrowser tb)
        {
            InitializeComponent();
            initialise_timers();
            DoingTaskBuildList = new List<TravianDoingTaskBuild>();
            TaskBuildBuildList = new List<TravianTaskBuild>();
            VillageList = new List<TravianVillage>();
            this.tb = tb;
            counter = 0;
            timerlbl.Text = counter.ToString();
        }
        public void initialise_timers()
        {
            SysTime = DateTime.Now.TimeOfDay;
            timer = new SysTimer();
            ResourceRefreshTimer = new SysTimer();
            TasksTimer = new SysTimer();
            TasklistRefreshTimer = new SysTimer();
            timer.Interval = 1000;
            TasksTimer.Interval = 1000;
            ResourceRefreshTimer.Interval = 15000;
            TasklistRefreshTimer.Interval = 5000;
            timer.Enabled = false;
            ResourceRefreshTimer.Enabled = false;
            TasksTimer.Enabled = true;
            TasklistRefreshTimer.Enabled = true;
            timer.Tick += new EventHandler(Timer_Tick);
            ResourceRefreshTimer.Tick += new EventHandler(ResourceTimerTick);
            TasksTimer.Tick += new EventHandler(Tasks_Timer_Tick);
            TasklistRefreshTimer.Tick += new EventHandler(Task_List_Refresh_Tick);
        }

        private void ResourceTimerTick(object sender, EventArgs e)
        {
            VillageView.Items.Clear();
            int i = 1;
            try
            {
                foreach (var village in VillageList)
                {
                    village.Resources[0] = village.Resources[0] + (village.Production[0] / 240);
                    village.Resources[1] = village.Resources[1] + (village.Production[1] / 240);
                    village.Resources[2] = village.Resources[2] + (village.Production[2] / 240);
                    village.Resources[3] = village.Resources[3] + (village.Production[3] / 240);

                    var lvi = new ListViewItem();
                    lvi.Text = village.Name;
                    lvi.SubItems.Add(i.ToString());
                    lvi.SubItems.Add(tb.Is_under_attack());
                    lvi.SubItems.Add(TaskBusyBool[village.Number - 1, 1].ToString());
                    lvi.SubItems.Add(TaskBusyBool[village.Number - 1, 0].ToString());
                    lvi.Tag = village.Number;
                    VillageView.Items.Add(lvi);
                }
                RefreshCurrentResources(CurrentVillage);
            }
            catch { }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ResourceTimerTick(sender, e);
        }

        private void Task_List_Refresh_Tick(object sender, EventArgs e)
        {
            RefreshTaskList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ResourceRefreshTimer.Enabled = true;
                TasksWorkerThread = new Thread(refreshVillages);
                TasksWorkerThread.Start();

                RefreshBuildingNowList();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR REFRESHING VILLAGES, this sometimes happen, refresh again"); }
        }

        private void refreshVillages()
        {
            VillageView.Invoke((MethodInvoker)delegate
            {
                VillageView.Items.Clear();
            });

            int i = 0;
            VillageList = new List<TravianVillage>();
            VillageList.AddRange(tb.RefreshVillageList());

            // TODO: MAKE ROMANS OR NON ROMANS WORK
            if (IsRoman.Checked == true)
            {
                TaskBusyBool = new bool[VillageList.Count, 2];
            }
            else
            {
                TaskBusyBool = new bool[VillageList.Count, 2];
            }

            foreach (TravianVillage village in VillageList)
            {
                i++;

                village.Number = i;
                tb.Navigate(village.Url);

                village.ResourceFields = tb.RefreshResourceAreas();

                tb.resources(village);

                village.Buildings = tb.RefreshBuildingAreas();

                var list =tb.RefreshBuildTimes(i);
                DoingTaskBuildList.AddRange(list);

                foreach(var item in list)
                {
                    MessageBox.Show(TaskBusyBool.GetLength(0) + "\n" + TaskBusyBool.GetLength(1));

                    MessageBox.Show((i-1).ToString());
                    MessageBox.Show((item.IsResource ? 1 : 0).ToString());
                    MessageBox.Show(TaskBusyBool[i-1, item.IsResource ? 1 : 0].ToString());

                    TaskBusyBool[i-1, item.IsResource ? 1 : 0] = true;

                }
            }

            foreach (var village in VillageList)
            {
                village.Resources[0] = village.Resources[0] + (village.Production[0] / 240);
                village.Resources[1] = village.Resources[1] + (village.Production[1] / 240);
                village.Resources[2] = village.Resources[2] + (village.Production[2] / 240);
                village.Resources[3] = village.Resources[3] + (village.Production[3] / 240);

                var lvi = new ListViewItem();
                lvi.Text = village.Name;
                lvi.SubItems.Add(i.ToString());
                lvi.SubItems.Add(tb.Is_under_attack());
                lvi.SubItems.Add(TaskBusyBool[village.Number - 1, 1].ToString());
                lvi.SubItems.Add(TaskBusyBool[village.Number - 1, 0].ToString());
                lvi.Tag = village.Number;
                VillageView.Invoke((MethodInvoker)delegate
                {
                    VillageView.Items.Add(lvi);
                });

            }

        }

        private void RefreshBuildingNowList()
        {
            BuildingNow.Items.Clear();

            foreach (TravianDoingTaskBuild task in DoingTaskBuildList)
            {
                ListViewItem lwi = new ListViewItem(task.VillageId.ToString());
                lwi.SubItems.Add(task.TimeLeft.ToString());
                lwi.SubItems.Add(task.Level.ToString());
                lwi.SubItems.Add(task.Name);
                lwi.SubItems.Add(task.IsResource.ToString());
                lwi.Tag = task;

                BuildingNow.Items.Add(lwi);
            }
        }

        private void RefreshBuildings(List<Building> buildings)
        {
            BuildingListView.Items.Clear();

            foreach (Building building in buildings)
            {
                ListViewItem lvi = new ListViewItem(building.name);
                lvi.SubItems.Add(building.level.ToString());
                lvi.Tag = building;

                BuildingListView.Items.Add(lvi);
            }
        }

        private void RefreshResources(List<Building> elements)
        {
            ResourceListView.Items.Clear();
            
            elements = elements.OrderBy(s => s.name).ThenBy(s => s.level).ToList();

            foreach (Building element in elements)
            {
                ListViewItem lvi = new ListViewItem(element.name);
                lvi.SubItems.Add(element.level.ToString());
                lvi.Tag = element;

                ResourceListView.Items.Add(lvi);
            }
        }

        private void RefreshCurrentResources(TravianVillage village)
        {
            try {
                ResourceMonitorView.Items.Clear();

                double[] warefull = new double[4];
                string[] names = { "Wood", "Molis", "Iron", "Wheat" };

                for (int i = 0; i < 3; i++)
                {
                    var lvi = new ListViewItem();
                    lvi.Text = names[i];
                    lvi.SubItems.Add(Math.Round(village.Resources[i],0).ToString() + " / " + village.Storage[0].ToString());

                    lvi.SubItems.Add(Math.Round(Convert.ToDouble(village.Resources[i]) * 100 / Convert.ToDouble(village.Storage[0]), 1).ToString() + " %");
                    lvi.SubItems.Add(Math.Round((Convert.ToDouble(village.Storage[0]) - Convert.ToDouble(village.Resources[i])) / Convert.ToDouble(village.Production[i]), 1).ToString() + "Hours");
                    lvi.SubItems.Add(village.Production[i].ToString());
                    ResourceMonitorView.Items.Add(lvi);
                }
                var lvii = new ListViewItem();
                lvii.Text = names[3];
                lvii.SubItems.Add(Math.Round(village.Resources[3],0) + " / " + village.Storage[1]);
                lvii.SubItems.Add(Math.Round(Convert.ToDouble(village.Resources[3]) * 100 / Convert.ToDouble(village.Storage[1]), 1).ToString() + " %");
                lvii.SubItems.Add(Math.Round((Convert.ToDouble(village.Storage[1]) - Convert.ToDouble(village.Production[3])) / Convert.ToDouble(village.Production[3]), 1).ToString() + "Hours");
                lvii.SubItems.Add(village.Production[3].ToString());
                ResourceMonitorView.Items.Add(lvii);
            }
            catch { }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            counter++;
            timerlbl.Text = counter.ToString();

            if (counter >= TasksInterval)
            {
                counter = 0;
                timerlbl.Text = "DOING TASKS";
                if (BuildingTasks.Items.Count == 0)
                    return;
                timer.Enabled = false;

                TasksWorkerThread = new Thread(Do_Tasks);
                TasksWorkerThread.Start();
                timer.Enabled = true;
            }
        }

        private void Tasks_Timer_Tick(object sender, EventArgs e)
        {

            SysTimeLabel.Text = SysTime.ToString(@"hh\:mm\:ss");
            SysTime = SysTime.Add(new TimeSpan(0, 0, 1));
            forcerefreshtimer--;
            BuildingNow.Items.Clear();
            int[] Remove = new int[] { 0, 0 };
            try
            {
                foreach (TravianDoingTaskBuild item in DoingTaskBuildList)
                {

                    item.TimeLeft = item.TimeLeft.Subtract(new TimeSpan(0, 0, 1));

                    int compare = TimeSpan.Compare(item.TimeLeft, new TimeSpan(0, 0, -1));
                    if (compare < 0)
                    {

                        Remove[0] = 1;
                        Remove[1] = DoingTaskBuildList.IndexOf(item);
                    }
                }
                if (Remove[0] == 1)
                {
                    TravianDoingTaskBuild DoneTask = DoingTaskBuildList.ElementAt(Remove[1]);

                    TaskBusyBool[DoneTask.VillageId-1, DoneTask.IsResource? 1 : 0] = false;
                    var task = TaskBuildBuildList.Find(t => t.href == DoneTask.url);
                    try
                    {
                        task.level++;
                    }
                    catch { }

                    var village = VillageList.Find(vil => vil.Number == DoneTask.VillageId);
                    var item = (village.Buildings.Concat(village.ResourceFields)).SingleOrDefault(x => x.href == DoneTask.url);
                    try
                    {
                        item.level++;
                    }
                    catch
                    {
                        if (DoneTask.IsResource)
                        {
                            tb.Navigate(VillageList[DoneTask.VillageId - 1].Url);
                            VillageList[DoneTask.VillageId - 1].ResourceFields = tb.RefreshResourceAreas();
                        }
                        else
                        {
                            tb.Navigate(VillageList[DoneTask.VillageId - 1].Url);
                            VillageList[DoneTask.VillageId - 1].ResourceFields = tb.RefreshBuildingAreas();
                        }
                    }
                    DoingTaskBuildList.RemoveAt(Remove[1]);
                }
            }
            catch (Exception ex) {
                ErrorConsole.Text += "\n\nERROR IN TASKS_TIMEER_TICK" + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace;
            }
            finally { RefreshBuildingNowList(); }
            try
            {
                if (forcerefreshtimer < 0)
                {
                    forcerefreshtimer = 300;
                    for (int j = 0; j < TaskBusyBool.GetLength(0); j++)
                    {
                        for (int k = 0; k < (TaskBusyBool.GetLength(1)-1); k++)
                        {
                            TaskBusyBool[j, k] = false;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                ErrorConsole.Text += "\n\nERROR IN TRYING TO FORCE REFRESH: " + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace;
            }
        }

        private void OpenFieldBtn_Click(object sender, EventArgs e)
        {
            try{ tb.Navigate(((Building)ResourceListView.SelectedItems[0].Tag).href); }
            catch{
                MessageBox.Show("Please select a field");
            }
        }

        private void OpenBuildingBtn_click(object sender, EventArgs e)
        {
            try{  tb.Navigate(((Building)BuildingListView.SelectedItems[0].Tag).href);  }
            catch{
                MessageBox.Show("Please select a field");
            }
        }

        private void VillageView_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int i = VillageView.Items.IndexOf(VillageView.SelectedItems[0]);
                CurrentVillage = VillageList[i];
                RefreshResources(CurrentVillage.ResourceFields);
                RefreshBuildings(CurrentVillage.Buildings);
                RefreshCurrentResources(CurrentVillage);
            }
            catch  { }
        }

        public void Do_Tasks()
        {
            bool NeedDorf1 = false;
            int[] Remove = new int[] { 0, 0 };
            try
            {
                foreach (TravianTaskBuild task in TaskBuildBuildList)
                {
                    if (TaskBusyBool[task.villageId - 1, task.IsResource ? 1 : 0])
                    {
                        continue;
                    }

                    TravianTaskAdded result = tb.DoBuildUpgrade(task);
                    switch (result.Status)
                    {
                        case "Done":
                            Remove[0] = task.villageId;
                            Remove[1] = TaskBuildBuildList.IndexOf(task);
                            NeedDorf1 = true;
                            break;
                        case "Busy":
                            task.Status = "Busy";
                            TaskBusyBool[task.villageId - 1, task.IsResource ? 1 : 0] = true;
                            NeedDorf1 = true;
                            continue;
                        case "Doing":
                            task.Status = "Doing";
                            TravianDoingTaskBuild tdtb = new TravianDoingTaskBuild(TimeSpan.Parse(result.Time), result.level, task.villageId, task.name, task.IsResource, task.href);
                            try
                            {
                                TaskBusyBool[task.villageId - 1, task.IsResource ? 1 : 0] = true;
                            }
                            catch { ErrorConsole.Text += "Error freezing buildspace in vil: " + task.villageId + "Resource: " + task.IsResource; }
                            
                            DoingTaskBuildList.Add(tdtb);
                            //TODO: FIX THIS< SO VILLAGE COMES FROM INDEX OF TASK -> REFRESH HIS RESOURCES

                            VillageList[tdtb.VillageId-1] = tb.resources(VillageList[tdtb.VillageId - 1]);
                            NeedDorf1 = false;
                            break;
                        case "Need Resources":
                            task.Status = "Need Resources";
                            TaskBusyBool[task.villageId - 1, task.IsResource ? 1 : 0] = true;
                            NeedDorf1 = true;
                            continue;
                        default:
                            task.Status = "ERROR XD";
                            break;
                    }
                }
                if (Remove[0] != 0)
                    TaskBuildBuildList.RemoveAt(Remove[1]);

                // TODO: JEIGU PASKUTINIS INDEXAS TAI TIK TADA LEIDZIA DORF1
                if (NeedDorf1)
                    tb.dorf1();
                return;
            }
            catch (Exception ex)
            {
                ErrorConsole.Invoke((MethodInvoker)delegate
                {
                    ErrorConsole.Invoke((MethodInvoker)delegate
                    {
                        ErrorConsole.Text += "\n\nERROR IN DO TASKS" + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace;
                    });
                });
                try
                {
                    ErrorConsole.Invoke((MethodInvoker)delegate
                    {
                        ErrorConsole.Text += "\n\n TRYING LOGIN";
                    });
                    tb.LoginTravian();
                }
                catch
                {
                    ErrorConsole.Invoke((MethodInvoker)delegate
                    {
                        ErrorConsole.Text += "\n\nERROR LOL UR FKED" + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace;
                    });
                }
                return;
            }
        }
        
        private void StartBotBtn_Click(object sender, EventArgs e)
        {
            TasksInterval = Convert.ToInt16(RefreshTimerSlct.Value);
            counter = 0;
            RefreshTimerSlct.ReadOnly = true;
            RefreshTimerSlct.Increment = 0;
            StartBotBtn.Enabled = false;

            TasksWorkerThread = new Thread(Do_Tasks);
            TasksWorkerThread.Start();

            timer.Enabled = true;
            TasklistRefreshTimer.Enabled = true;
        }

        private void RefreshTaskList()
        {
            BuildingTasks.Items.Clear();
            foreach (TravianTaskBuild buildTask in TaskBuildBuildList)
            {
                string id = buildTask.villageId.ToString();
                ListViewItem lwi = new ListViewItem(id);
                lwi.SubItems.Add(buildTask.level.ToString());
                lwi.SubItems.Add(buildTask.DesiredLvl.ToString());
                lwi.SubItems.Add(buildTask.name);
                lwi.SubItems.Add(buildTask.Status);

                BuildingTasks.Items.Add(lwi);
            }
        }







        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            MapListView.Items.Clear();
        }

        private void SearchMap_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            forcerefreshduration = Convert.ToUInt16(numericUpDown2.Value);
        }

        private void TaskBuildBtn_Click(object sender, EventArgs e)
        {
            try { Add_task_to_tasklist((Building)BuildingListView.SelectedItems[0].Tag,false); }
            catch (Exception ex) { ErrorConsole.Text += "\n\nERROR ADDING FIELD TO UPDATE LISTVIEW" + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace; }
            finally { RefreshTaskList(); }
        }

        private void TaskFieldBtn_Click(object sender, EventArgs e)
        {
            try { Add_task_to_tasklist((Building)ResourceListView.SelectedItems[0].Tag,true); }
            catch (Exception ex) { ErrorConsole.Text += "\n\nERROR ADDING FIELD TO UPDATE LISTVIEW" + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace; }
            finally { RefreshTaskList(); }
        }

        private void Add_task_to_tasklist(Building building, bool Build_or_field)
        {
            var Task = new TravianTaskBuild(building.name, building.href, building.level, CurrentVillage.Url, CurrentVillage.Number, (int)numericUpDown1.Value, "New", Build_or_field);
            TaskBuildBuildList.Add(Task);
        }

        private void StopBotBtn_Click(object sender, EventArgs e)
        {
            StartBotBtn.Enabled = true;
            RefreshTimerSlct.ReadOnly = false;
            RefreshTimerSlct.Increment = 1;
            timer.Enabled = false;
            TasklistRefreshTimer.Enabled = false;
            timerlbl.Text = "Stopped";
        }



        private void button2_Click(object sender, EventArgs e)
        {
            foreach (TravianDoingTaskBuild item in DoingTaskBuildList)
            {
                item.TimeLeft = item.TimeLeft.Subtract(new TimeSpan(0, 0, 10));
            }
        }
        void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.FormOwnerClosing)
                this.Owner.Close();
            tb.close();
        }

    }
}
