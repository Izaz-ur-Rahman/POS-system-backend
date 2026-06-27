// export default function LowStockAlert({ data }) {

//     if (!data.length) return null;

//     return (
//         <div className="alert-box">

//             <h3>⚠ Low Stock Alerts</h3>

//             {data.map(item => (
//                 <div key={item.productId} className="alert-item">
//                     {item.productName} - Stock: {item.currentStock}
//                 </div>
//             ))}

//         </div>
//     );
// }

export default function LowStockAlerts({ data }) {

    return (
        <div className="table-box">

            <h3>Low Stock Alerts</h3>

            <table>

                <thead>
                    <tr>
                        <th>Product</th>
                        <th>Stock</th>
                        <th>Min</th>
                    </tr>
                </thead>

                <tbody>
                    {data.map((p, i) => (
                        <tr key={i}>
                            <td>{p.productName}</td>
                            <td>{p.currentStock}</td>
                            <td>{p.minStockLevel}</td>
                        </tr>
                    ))}
                </tbody>

            </table>

        </div>
    );
}