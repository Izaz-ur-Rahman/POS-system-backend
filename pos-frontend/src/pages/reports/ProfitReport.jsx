import { useEffect, useState } from "react";
import api from "../../api/axios";
import {
    LineChart,
    Line,
    XAxis,
    YAxis,
    Tooltip,
    ResponsiveContainer
} from "recharts";

export default function ProfitReport() {

    const [data, setData] = useState([]);

    useEffect(() => {
        api.get("/inventory/profit/daily")
            .then(res => setData(res.data));
    }, []);

    return (
        <div className="report-page">

            <h2>📈 Profit Report</h2>

            <ResponsiveContainer width="100%" height={400}>
                <LineChart data={data}>

                    <XAxis
                        dataKey="date"
                        tickFormatter={(d) =>
                            new Date(d).toLocaleDateString()
                        }
                    />

                    <YAxis />
                    <Tooltip />

                    <Line
                        type="monotone"
                        dataKey="profit"
                        stroke="#22c55e"
                    />

                </LineChart>
            </ResponsiveContainer>

        </div>
    );
}