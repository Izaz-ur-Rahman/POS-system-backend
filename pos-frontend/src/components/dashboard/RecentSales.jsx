export default function RecentSales({ data }) {
    return (
        <div className="recent-sales">
            <h3>Recent Sales</h3>

            <table>
                <thead>
                    <tr>
                        <th>Invoice</th>
                        <th>Customer</th>
                        <th>Total</th>
                    </tr>
                </thead>

                <tbody>
                    {data?.map(s => (
                        <tr key={s.id}>
                            <td>{s.invoiceNo}</td>
                            <td>{s.customerName}</td>
                            <td>{s.totalAmount}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}