import { useEffect, useState } from "react";
import api from "../../api/axios";

export default function InventoryReport() {

    const [data, setData] = useState(null);

    useEffect(() => {
        api.get("/inventory/valuation")
            .then(res => setData(res.data));
    }, []);

    return (
        <div className="report-page">

            <h2>🏬 Inventory Valuation</h2>

            {data && (
                <div className="report-card">
                    <p>Total Products Value: {data.totalInventoryValue} PKR</p>
                </div>
            )}

        </div>
    );
}