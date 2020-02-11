using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeknikEnvanter2
{
    public partial class Form1 : Form
    {
        public List<Event> eventList = new List<Event>();

        private System.Timers.Timer eventProducerTimer;

        private System.Timers.Timer eventEventComsumer;

        private DateTime firstDate;

        private DateTime endDate;

        public int EventProducerNumber = 0;

        private int lastControlledNumber = 0;

        private int alertNumber = 1;

        bool isEventComsumerAsyncStarted = false;

        private Event ReadEvent(int eventId)
        {
            return eventList[eventId] as Event;
        }

        private void EventProducer()
        {
            

            // Create a timer and set a two second interval.
            eventProducerTimer = new System.Timers.Timer();
            eventProducerTimer.Interval = 3000;

            // Hook up the Elapsed event for the timer. 
            eventProducerTimer.Elapsed += OnTimedEventProducer;

            // Have the timer fire repeated events (true is the default)
            eventProducerTimer.AutoReset = true;

            // Start the timer
            eventProducerTimer.Enabled = true;

        }

        private async Task EventComsumerAsync()
        {
            // Create a timer and set a two second interval.
            eventEventComsumer = new System.Timers.Timer();
            eventEventComsumer.Interval = 5000;

            // Hook up the Elapsed event for the timer. 
            eventEventComsumer.Elapsed += OnTimedEventComsumer;

            // Have the timer fire repeated events (true is the default)
            eventEventComsumer.AutoReset = true;

            // Start the timer
            eventEventComsumer.Enabled = true;
        }

        private void OnTimedEventProducer(Object source, System.Timers.ElapsedEventArgs e)
        {
            if ((EventProducerNumber) < 400)
            {
                EventProducerNumber++;

                // ... Create new Random object.
                Random random = new Random();

                eventList.Add(new Event { Priority = random.Next(1, 4) });

               

                if (((EventProducerNumber+1)/4)> progressBar1.Value)
                {
                    progressBar1.Value++;
                }

               if(EventProducerNumber > 2 && !isEventComsumerAsyncStarted)
                {
                    isEventComsumerAsyncStarted = true;
                    lastControlledNumber = EventProducerNumber;
                    EventComsumerAsync();
                }

                if ((EventProducerNumber + 1) == 400)
                {
                    eventProducerTimer.AutoReset = false;
                }
            }

        }

        private void OnTimedEventComsumer(Object source, System.Timers.ElapsedEventArgs e)
        {
            // int lastIndex = eventList.Count -1;

            if (lastControlledNumber+1 == 400)
            {
                progressBar2.Maximum = EventProducerNumber;

                progressBar2.Value = lastControlledNumber+1;

                lblProgressBar2.Text = (lastControlledNumber + 1).ToString() + " / " + EventProducerNumber.ToString();

                eventEventComsumer.AutoReset = false;

                return;
            }

            Event event1 = ReadEvent(lastControlledNumber);

            Event event2 = ReadEvent(lastControlledNumber - 1);

            Event event3 = ReadEvent(lastControlledNumber - 2);

            lastControlledNumber++;

            if (event1.Priority == event2.Priority && event1.Priority == event3.Priority)
            {
                Console.WriteLine($"ALERT {(alertNumber).ToString()}: E{(lastControlledNumber - 2).ToString()}, E{(lastControlledNumber - 1).ToString()}, E{(lastControlledNumber).ToString()} priorty level same");
                alertNumber++;
            }
            else if (event2.Priority != event3.Priority && EventProducerNumber > lastControlledNumber)
            {
                lastControlledNumber++;
            }


            // Display the ProgressBar control.
            progressBar2.Visible = true;
            lblProgressBar2.Visible = true;
            label2.Visible = true;

            // Set Maximum to the total number of files to copy.
            progressBar2.Maximum = EventProducerNumber;

            progressBar2.Value = lastControlledNumber;

            lblProgressBar2.Text = $"{lastControlledNumber.ToString()} / {EventProducerNumber.ToString()}";

        }


        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;

            InitializeComponent();
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            //ProgressBar
            // Display the ProgressBar control.
            progressBar1.Visible = true;
            // Set Minimum to 1 to represent the first file being copied.
            progressBar1.Minimum = 1;
            // Set Maximum to the total number of files to copy.
            progressBar1.Maximum = 100;
            // Set the initial value of the ProgressBar.
            progressBar1.Value = 1;
            // Set the Step property to a value of 1 to represent each file being copied.
            progressBar1.Step = 1;

            //ProgressBar

            progressBar2.Visible = false;
            lblProgressBar2.Visible = false;
            label2.Visible = false;

            // Set Minimum to 1 to represent the first file being copied.
            progressBar2.Minimum = 0;
            // Set Maximum to the total number of files to copy.
            progressBar2.Value = 0;
            // Set the Step property to a value of 1 to represent each file being copied.
            progressBar2.Step = 1;
            //

            EventProducer();

            timer1.Start();

            endDate = DateTime.Now.AddMinutes(20);

            firstDate = DateTime.Now;

            btnStart.Visible = false;
        }

        private void lblTimer_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan span = (endDate - firstDate);
            lblTimer.Text = String.Format("{0} dk {1} sn!", span.Minutes, span.Seconds).ToString();
            progressBar2.Maximum = EventProducerNumber;
            lblProgressBar2.Text = lastControlledNumber.ToString() + " / " + EventProducerNumber.ToString();
            firstDate = firstDate.AddSeconds(1);
            if (span < TimeSpan.FromSeconds(1))
            {
                timer1.Stop();
            }

            

        }
    }
}
