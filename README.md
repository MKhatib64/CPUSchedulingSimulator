# CPU Scheduling Simulator

This project is a CPU Scheduling Simulator implemented in C#. It evaluates various CPU scheduling algorithms to determine their performance in terms of waiting time, turnaround time, and other metrics. The simulator includes the following scheduling algorithms:
- First Come, First Served (FCFS)
- Shortest Job First (SJF)
- Round Robin (RR)
- Priority Scheduling
- Shortest Remaining Time First (SRTF)
- Multi-Level Feedback Queue (MLFQ)

## Table of Contents
- Features
- Prerequisites
- Setup and Installation
- Running the Simulator
- Example Output
- Project Structure
- Performance Metrics
- Contributing
- License
- Acknowledgments
- Contact

## Features
- Six Scheduling Algorithms: FCFS, SJF, RR, Priority Scheduling, SRTF, and MLFQ
- Performance Metrics: Calculates waiting time, turnaround time, and other metrics for each algorithm
- Console-Based Interface: Easy-to-use console interface for running simulations
- Extensible: Add new scheduling algorithms or modify existing ones

## Prerequisites
To run this project, you need:
- .NET SDK (version 5.0 or higher)
- A code editor like Visual Studio or Visual Studio Code

## Setup and Installation
Clone the repository:
git clone https://github.com/your-username/CPU-Scheduling-Simulator.git

Navigate to the project directory:
cd CPU-Scheduling-Simulator

Build the project:
dotnet build

## Running the Simulator
Run the project:
dotnet run

The simulator will execute all scheduling algorithms and display the results in the console.

## Example Output
Results for FCFS:
ID      Waiting Time    Turnaround Time
1       0               6
2       5               13
3       12              19
4       16              19

Results for SJF:
ID      Waiting Time    Turnaround Time
1       0               6
2       6               14
3       14              21
4       3               6

## Project Structure
CPU-Scheduling-Simulator/
├── Program.cs          # Main entry point for the application
├── Scheduler.cs        # Contains all scheduling algorithms
├── Process.cs          # Defines the Process class
└── README.md           # Project documentation

## Performance Metrics
The simulator calculates the following metrics for each algorithm:
- Average Waiting Time (AWT): The average time processes spend waiting in the ready queue
- Average Turnaround Time (ATT): The average time taken for processes to complete execution
- CPU Utilization (%): The percentage of time the CPU is busy executing processes
- Throughput: The number of processes completed per unit time

## Contributing
Contributions are welcome! If you'd like to contribute:
1. Fork the repository
2. Create a new branch for your feature or bugfix
3. Submit a pull request

## License
This project is licensed under the MIT License. See the LICENSE file for details.

## Acknowledgments
This project was inspired by the need to evaluate CPU scheduling algorithms for performance optimization.
Special thanks to the .NET community for providing excellent tools and resources.

## Contact
For questions or feedback, please contact: Mohamed Khatib  
Email: mkhatib2@students.kennesaw.edu  
GitHub: Mkhatib64
