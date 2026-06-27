// export default function TopProducts({ data }) {

//     return (
//         <div className="panel">

//             <h3>🔥 Top Selling Products</h3>

//             <table>
//                 <thead>
//                     <tr>
//                         <th>Product</th>
//                         <th>Qty Sold</th>
//                         <th>Revenue</th>
//                     </tr>
//                 </thead>

//                 <tbody>
//                     {data.map(p => (
//                         <tr key={p.productId}>
//                             <td>{p.productName}</td>
//                             <td>{p.totalQuantitySold}</td>
//                             <td>{p.totalRevenue}</td>
//                         </tr>
//                     ))}
//                 </tbody>
//             </table>

//         </div>
//     );
// }
export default function TopProducts({ data }) {

    return (
        <div className="table-box">

            <h3>Top Products</h3>

            <table>

                <thead>
                    <tr>
                        <th>Product</th>
                        <th>Qty</th>
                        <th>Revenue</th>
                    </tr>
                </thead>

                <tbody>
                    {data.map((p, i) => (
                        <tr key={i}>
                            <td>{p.productName}</td>
                            <td>{p.totalQuantitySold}</td>
                            <td>{p.totalRevenue}</td>
                        </tr>
                    ))}
                </tbody>

            </table>

        </div>
    );
}