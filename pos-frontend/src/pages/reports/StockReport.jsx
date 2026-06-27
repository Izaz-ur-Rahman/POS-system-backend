import { useEffect, useState } from "react";
import api from "../../api/axios";

export default function StockReport() {

    const [data, setData] = useState([]);

    useEffect(() => {
        api.get("/stock")
            .then(res => setData(res.data));
    }, []);

    return (
        <div className="report-page">

            <h2>📦 Stock Report</h2>

            <table>
                <thead>
                    <tr>
                        <th>Product</th>
                        <th>Stock</th>
                        <th>Min Level</th>
                        <th>Status</th>
                    </tr>
                </thead>

                <tbody>
                    {data.map((item, i) => (
                        <tr key={i}>
                            <td>{item.productName}</td>
                            <td>{item.stockQuantity}</td>
                            <td>{item.minStockLevel}</td>
                            <td style={{ color: item.isLowStock ? "red" : "green" }}>
                                {item.isLowStock ? "LOW" : "OK"}
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>

        </div>
    );
}