export default function LiveNotifications({ data }) {
    return (
        <div className="live-notifications">
            <h3>Live Notifications</h3>

            {data?.slice(0, 5).map((n, i) => (
                <div key={i} className="notification">
                    🔔 {n}
                </div>
            ))}
        </div>
    );
}