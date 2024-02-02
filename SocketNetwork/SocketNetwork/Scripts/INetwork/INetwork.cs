﻿namespace Network
{
    public interface INetwork
    {
        void SetData(NetworkData networkData);

        void Start();

        void Send(string msg);
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