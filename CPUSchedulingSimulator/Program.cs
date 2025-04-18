using System;
using System.Collections.Generic;
using System.Linq;

namespace CPUSchedulingSimulator
{
    public class Process
    {
        public int Id { get; set; }
        public int ArrivalTime { get; set; }
        public int BurstTime { get; set; }
        public int Priority { get; set; }
        public int RemainingTime { get; set; }
        public int WaitingTime { get; set; }
        public int TurnaroundTime { get; set; }
    }

    public class Scheduler
    {
        public static void FCFS(List<Process> processes)
        {
            processes.Sort((p1, p2) => p1.ArrivalTime.CompareTo(p2.ArrivalTime));
            int currentTime = 0;
            foreach (var process in processes)
            {
                process.WaitingTime = currentTime - process.ArrivalTime;
                currentTime += process.BurstTime;
                process.TurnaroundTime = currentTime - process.ArrivalTime;
            }
        }

        public static void SJF(List<Process> processes)
        {
            processes.Sort((p1, p2) => p1.BurstTime.CompareTo(p2.BurstTime));
            int currentTime = 0;
            foreach (var process in processes)
            {
                process.WaitingTime = currentTime - process.ArrivalTime;
                currentTime += process.BurstTime;
                process.TurnaroundTime = currentTime - process.ArrivalTime;
            }
        }

        public static void RoundRobin(List<Process> processes, int quantum)
        {
            Queue<Process> queue = new Queue<Process>(processes);
            int currentTime = 0;
            while (queue.Count > 0)
            {
                var process = queue.Dequeue();
                int timeSlice = Math.Min(quantum, process.RemainingTime);
                process.RemainingTime -= timeSlice;
                currentTime += timeSlice;
                if (process.RemainingTime > 0)
                    queue.Enqueue(process);
                else
                {
                    process.TurnaroundTime = currentTime - process.ArrivalTime;
                    process.WaitingTime = process.TurnaroundTime - process.BurstTime;
                }
            }
        }

        public static void PriorityScheduling(List<Process> processes)
        {
            processes.Sort((p1, p2) => p1.Priority.CompareTo(p2.Priority));
            int currentTime = 0;
            foreach (var process in processes)
            {
                process.WaitingTime = currentTime - process.ArrivalTime;
                currentTime += process.BurstTime;
                process.TurnaroundTime = currentTime - process.ArrivalTime;
            }
        }

        public static void SRTF(List<Process> processes)
        {
            List<Process> remainingProcesses = new List<Process>(processes);
            int currentTime = 0;
            while (remainingProcesses.Count > 0)
            {
                var nextProcess = remainingProcesses
                    .Where(p => p.ArrivalTime <= currentTime)
                    .OrderBy(p => p.RemainingTime)
                    .FirstOrDefault();
                if (nextProcess == null)
                {
                    currentTime++;
                    continue;
                }
                nextProcess.RemainingTime--;
                currentTime++;
                if (nextProcess.RemainingTime == 0)
                {
                    nextProcess.TurnaroundTime = currentTime - nextProcess.ArrivalTime;
                    nextProcess.WaitingTime = nextProcess.TurnaroundTime - nextProcess.BurstTime;
                    remainingProcesses.Remove(nextProcess);
                }
            }
        }

        public static void MLFQ(List<Process> processes)
        {
            Queue<Process> highPriorityQueue = new Queue<Process>();
            Queue<Process> mediumPriorityQueue = new Queue<Process>();
            Queue<Process> lowPriorityQueue = new Queue<Process>();

            foreach (var process in processes)
                highPriorityQueue.Enqueue(process);

            int currentTime = 0;
            while (highPriorityQueue.Count > 0 || mediumPriorityQueue.Count > 0 || lowPriorityQueue.Count > 0)
            {
                Process nextProcess = null;
                if (highPriorityQueue.Count > 0)
                    nextProcess = highPriorityQueue.Dequeue();
                else if (mediumPriorityQueue.Count > 0)
                    nextProcess = mediumPriorityQueue.Dequeue();
                else if (lowPriorityQueue.Count > 0)
                    nextProcess = lowPriorityQueue.Dequeue();

                if (nextProcess != null)
                {
                    int timeSlice = GetTimeSliceForQueue(nextProcess);
                    int timeUsed = Math.Min(timeSlice, nextProcess.RemainingTime);
                    nextProcess.RemainingTime -= timeUsed;
                    currentTime += timeUsed;

                    if (nextProcess.RemainingTime > 0)
                    {
                        if (nextProcess.Priority > 1)
                            nextProcess.Priority--;
                        if (nextProcess.Priority == 2)
                            mediumPriorityQueue.Enqueue(nextProcess);
                        else if (nextProcess.Priority == 1)
                            lowPriorityQueue.Enqueue(nextProcess);
                    }
                    else
                    {
                        nextProcess.TurnaroundTime = currentTime - nextProcess.ArrivalTime;
                        nextProcess.WaitingTime = nextProcess.TurnaroundTime - nextProcess.BurstTime;
                    }
                }
            }
        }

        private static int GetTimeSliceForQueue(Process process)
        {
            if (process.Priority == 3)
                return 2; // High priority queue
            else if (process.Priority == 2)
                return 4; // Medium priority queue
            else
                return 8; // Low priority queue
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Process> processes = new List<Process>
            {
                new Process { Id = 1, ArrivalTime = 0, BurstTime = 6, Priority = 3, RemainingTime = 6 },
                new Process { Id = 2, ArrivalTime = 1, BurstTime = 8, Priority = 2, RemainingTime = 8 },
                new Process { Id = 3, ArrivalTime = 2, BurstTime = 7, Priority = 1, RemainingTime = 7 },
                new Process { Id = 4, ArrivalTime = 3, BurstTime = 3, Priority = 3, RemainingTime = 3 }
            };

            // Clone processes for each algorithm
            var processesFCFS = processes.Select(p => new Process { Id = p.Id, ArrivalTime = p.ArrivalTime, BurstTime = p.BurstTime, Priority = p.Priority, RemainingTime = p.BurstTime }).ToList();
            var processesSJF = processes.Select(p => new Process { Id = p.Id, ArrivalTime = p.ArrivalTime, BurstTime = p.BurstTime, Priority = p.Priority, RemainingTime = p.BurstTime }).ToList();
            var processesRR = processes.Select(p => new Process { Id = p.Id, ArrivalTime = p.ArrivalTime, BurstTime = p.BurstTime, Priority = p.Priority, RemainingTime = p.BurstTime }).ToList();
            var processesPriority = processes.Select(p => new Process { Id = p.Id, ArrivalTime = p.ArrivalTime, BurstTime = p.BurstTime, Priority = p.Priority, RemainingTime = p.BurstTime }).ToList();
            var processesSRTF = processes.Select(p => new Process { Id = p.Id, ArrivalTime = p.ArrivalTime, BurstTime = p.BurstTime, Priority = p.Priority, RemainingTime = p.BurstTime }).ToList();
            var processesMLFQ = processes.Select(p => new Process { Id = p.Id, ArrivalTime = p.ArrivalTime, BurstTime = p.BurstTime, Priority = p.Priority, RemainingTime = p.BurstTime }).ToList();

            // Run algorithms
            Scheduler.FCFS(processesFCFS);
            Scheduler.SJF(processesSJF);
            Scheduler.RoundRobin(processesRR, 2);
            Scheduler.PriorityScheduling(processesPriority);
            Scheduler.SRTF(processesSRTF);
            Scheduler.MLFQ(processesMLFQ);

            // Display results
            DisplayResults("FCFS", processesFCFS);
            DisplayResults("SJF", processesSJF);
            DisplayResults("Round Robin", processesRR);
            DisplayResults("Priority Scheduling", processesPriority);
            DisplayResults("SRTF", processesSRTF);
            DisplayResults("MLFQ", processesMLFQ);
        }

        static void DisplayResults(string algorithm, List<Process> processes)
        {
            Console.WriteLine($"Results for {algorithm}:");
            Console.WriteLine("ID\tWaiting Time\tTurnaround Time");
            foreach (var process in processes)
            {
                Console.WriteLine($"{process.Id}\t{process.WaitingTime}\t\t{process.TurnaroundTime}");
            }
            Console.WriteLine();
        }
    }
}