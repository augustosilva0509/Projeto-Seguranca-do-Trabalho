using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace game
{
    public class Game
    {
        private int maxNumberOfCases = 4;
        private int numberOfProjects;
        public Project[] projects;

        private int timer;

        public bool timerEnded;
        public int maxTimer;
        public int caseNumber;
        public Result results;
        public Project currentCase;

        public int MaxNumberOfCases { get => maxNumberOfCases; }
        public int Timer { get => timer; set => timer = value; }

        public Game(int maxTimer, int numberOfProjects)
        {
            this.maxTimer = maxTimer;
            this.numberOfProjects = numberOfProjects;
            this.timer = this.maxTimer;
            this.caseNumber = 1;
            this.results = new Result(maxNumberOfCases);
            this.projects = new Project[this.numberOfProjects];
            this.projects = Project.InitializeProjects();
            this.currentCase = Project.GetRandomProject(this.projects, this.caseNumber);
            
        }
        public void UpdateCase(CheckedListBox clb, bool timesUp = false)
        {
            this.results.UpdateCase(this.currentCase, GetErrorIndices(clb, this.currentCase));
            this.currentCase = Project.GetRandomProject(this.projects, this.caseNumber);
            this.timer = this.maxTimer;
            this.caseNumber++;
        }
        private int[] GetErrorIndices(CheckedListBox clb, Project project)
        {
            HashSet<int> checkedIndices = new HashSet<int>(clb.CheckedIndices.Cast<int>());
            HashSet<int> projectIndices = new HashSet<int>(project.CheckIndexes);

            checkedIndices.SymmetricExceptWith(projectIndices);

            return checkedIndices.ToArray<int>();
        }
        public bool End()
        {
            if (this.caseNumber > maxNumberOfCases)
                return true;
            return false;
        }
    }
}
