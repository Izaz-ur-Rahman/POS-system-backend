import { useEffect, useState } from "react";
import api from "../../api/axios";

export default function SalesReport() {

    const [date, setDate] = useState("");
    const [data, setData] = useState(null);

    const loadReport = async () => {
        if (!date) return;

        const res = await api.get(`/report/daily-sales?date=${date}`);
        setData(res.data);
    };

    useEffect(() => {
        loadReport();
    }, [date]);

    return (
        <div className="report-page">

            <h2>📊 Sales Report</h2>

            <input
                type="date"
                onChange={(e) => setDate(e.target.value)}
            />

            {data && (
                <div className="report-card">
                    <p>Total Sales: {data.totalSales}</p>
                    <p>Total Transactions: {data.totalTransactions}</p>
                    <p>Cash Received: {data.totalCashReceived}</p>
                    <p>Profit: {data.totalProfit}</p>
                </div>
            )}

        </div>
    );
}