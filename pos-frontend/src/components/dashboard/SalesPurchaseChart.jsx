import {
    LineChart,
    Line,
    XAxis,
    YAxis,
    Tooltip,
    ResponsiveContainer
} from "recharts";

export default function SalesPurchaseChart({ data }) {

    return (
        <div className="chart-box">

            <h3>Sales Trend</h3>

            <ResponsiveContainer width="100%" height={300}>
                <LineChart data={data}>

                    <XAxis dataKey="date" />
                    <YAxis />
                    <Tooltip />

                    <Line type="monotone" dataKey="sales" stroke="#3b82f6" />

                </LineChart>
            </ResponsiveContainer>

        </div>
    );
}