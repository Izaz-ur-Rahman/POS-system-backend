import * as signalR from "@microsoft/signalr";

let connection = null;

export const startSignalRConnection = async (onMessageReceived) => {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("https://localhost:7154/notificationHub", {
            accessTokenFactory: () => localStorage.getItem("token")
        })
        .withAutomaticReconnect()
        .build();

    connection.on("ReceiveNotification", (message) => {
        console.log("Notification:", message);

        if (onMessageReceived) {
            onMessageReceived(message);
        }
    });

    try {
        await connection.start();
        console.log("SignalR Connected");
    } catch (err) {
        console.error("SignalR Connection Error:", err);
    }
};

export const stopSignalRConnection = async () => {
    if (connection) {
        await connection.stop();
        connection = null;
    }
};