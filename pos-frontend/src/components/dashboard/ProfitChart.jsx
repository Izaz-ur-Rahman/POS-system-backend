// import {
//     LineChart,
//     Line,
//     XAxis,
//     YAxis,
//     Tooltip,
//     ResponsiveContainer
// } from "recharts";

// export default function ProfitChart({ data }) {

//     if (!data || data.length === 0) return null;

//     return (
//         <div className="chart-box">

//             <h3>Profit Trend</h3>

//             <ResponsiveContainer width="100%" height={300}>
//                 <LineChart data={data}>

//                     <XAxis 
//                         dataKey="date"
//                         tickFormatter={(date) =>
//                             new Date(date).toLocaleDateString()
//                         }
//                     />

//                     <YAxis />

//                     <Tooltip />

//                     <Line
//                         type="monotone"
//                         dataKey="profit"
//                         stroke="#22c55e"
//                         strokeWidth={2}
//                     />

//                 </LineChart>
//             </ResponsiveContainer>

//         </div>
//     );
// }
import {
    LineChart,
    Line,
    XAxis,
    YAxis,
    Tooltip,
    ResponsiveContainer
} from "recharts";

export default function ProfitChart({ data }) {

    const chartData = Array.isArray(data) ? data : [];

    if (chartData.length === 0) return <p>No profit data</p>;

    return (
        <div className="chart-box">
            <h3>Profit Trend</h3>

            <ResponsiveContainer width="100%" height={300}>
                <LineChart data={chartData}>

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
                        strokeWidth={2}
                    />

                </LineChart>
            </ResponsiveContainer>
        </div>
    );
}