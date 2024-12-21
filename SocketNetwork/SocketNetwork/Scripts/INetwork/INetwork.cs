namespace Network
{
    public interface INetwork
    {
        void SetData(NetworkData networkData);

        void Start();

        void Send(string msg);

		void Dispose();

	}

	public enum NetWorkState
	{
		// init
		Closed,

		// client
		Connected,
		Connecting,
		ConnectFailed,

		// both
		Timeout,
		Disconnected,
		// server
		Listening,
	}
}