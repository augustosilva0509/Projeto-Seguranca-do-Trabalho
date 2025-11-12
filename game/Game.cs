using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace game
{
    public class Game
    {
        private const int maxNumberOfCases = 4;
        private Project[] projects = new Project[maxNumberOfCases];
        private int timer;

        public int maxTimer;
        public int caseNumber;
        public Result results;
        public Project currentCase;

        public Game(int maxTimer, Label lblTimer) 
        {
            this.maxTimer = maxTimer;
            this.timer = this.maxTimer;
            this.caseNumber = 1;
            this.results = new Result(maxNumberOfCases);
            this.projects = Project.InitializeProjects();
            this.currentCase = Project.GetRandomProject(this.projects, this.caseNumber);
            TimerCounter(lblTimer);
        }
        public void UpdateCase(CheckedListBox clb)
        {
            this.results.UpdateCase(this.currentCase, GetErrorIndices(clb, this.currentCase));
            this.currentCase = Project.GetRandomProject(this.projects, this.caseNumber);
            this.timer = this.maxTimer;
            this.caseNumber++;
        }
        private int[] GetErrorIndices(CheckedListBox clb, Project project)
        {
            HashSet<int> checkedIndices = new HashSet<int>(clb.CheckedIndices.Cast<int>());
            HashSet<int> projectIndices = new HashSet<int>(project.checkindexes);

            checkedIndices.SymmetricExceptWith(projectIndices);

            return checkedIndices.ToArray<int>();
        }
        private async void TimerCounter(Label lblTimer)
        {
            if (this.timer == 0)
            {
                MessageBox.Show("Tempo esgotado!");
                Application.Exit();
            }

            await Task.Delay(1000);
            this.timer--;
            lblTimer.Text = $"{this.timer}";
            TimerCounter(lblTimer);
        }
        public bool End()
        {
            if (this.caseNumber > maxNumberOfCases)
                return true;
            return false;
        }
    }
}
