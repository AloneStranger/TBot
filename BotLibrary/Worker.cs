using System;
using System.Threading;
using System.Collections.Generic;

namespace BotLibrary
{
    public static class Worker
    {
        private static volatile bool _shouldStop;
        private static volatile List<Task> Tasks;
        private static Thread WorkThread;
        private const int TimeOutInSec = 5;

        private static void DoWork()
        {
            while (!_shouldStop)
            {
                if ((Tasks == null) || (Tasks.Count == 0))
                {
                    WorkThread = null;
                    Tasks = null;
                    break;
                }

                foreach (Task t in Tasks)
                    if (DateTime.Now >= t.TimerDT)
                        DoTask(t);

                Thread.Sleep(TimeSpan.FromSeconds(TimeOutInSec));
            }
        }

        public static void Stop()
        {
            if ((WorkThread == null) || (WorkThread.ThreadState != ThreadState.Running))
                return;

            _shouldStop = true;
            WorkThread.Join(TimeSpan.FromSeconds(TimeOutInSec * 1.1));
            WorkThread = null;
            Tasks = null;
        }

        public static void AddTask(Task task)
        {
            Tasks.Add(task);
            if (WorkThread == null)
            {
                WorkThread = new Thread(DoWork);
                WorkThread.Start(ThreadPriority.BelowNormal);
            }
        }

        async private static void DoTask(Task task)
        {
            switch(task.mesageType)
            {
                case MType.Plain: await Main.Bot.SendTextMessageAsync(task.To, task.Message); break;
                case MType.Reply: await Main.Bot.SendTextMessageAsync(task.To, task.Message, replyToMessageId: task.MessageID); break;
                case MType.Forward: await Main.Bot.ForwardMessageAsync(task.To, task.From, task.MessageID); break;
            }
        }

    }

    public enum MType
    {
        Plain,
        Reply,
        Forward
    }

    public class Task
    {
        public DateTime TimerDT;
        public MType mesageType;
        public long From;
        public long To;
        public int MessageID;
        public string Message;
        public bool FromDB;
    }
}