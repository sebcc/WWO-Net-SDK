namespace WonderwareOnlineSDK.BackgroundTasks
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal class PurgeTask
    {
        private readonly CancellationToken cancellationToken;
        private readonly int repeatTime;
        private readonly Func<Task> purgeAction;

        public PurgeTask(CancellationToken cancellationToken, int repeatTime, Func<Task> purgeAction)
        {
            this.cancellationToken = cancellationToken;
            this.repeatTime = repeatTime;
            this.purgeAction = purgeAction;
            new Task(Execute, cancellationToken, TaskCreationOptions.LongRunning).Start();
        }

        private void Execute()
        {
            while (!this.cancellationToken.IsCancellationRequested)
            {
                try
                {
                    this.purgeAction().Wait(cancellationToken);
                }
                catch (AggregateException)
                {
                }
                this.cancellationToken.WaitHandle.WaitOne(repeatTime);
            }
        }
    }
}