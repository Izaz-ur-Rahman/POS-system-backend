import { useEffect, useState } from "react";
import MainLayout from "../layouts/MainLayout";
import api from "../api/axios";

import SaleForm from "../components/SaleForm";
import SaleDetails from "../components/SaleDetails";

import "../styles/pages/sale.css";

export default function Sale() {

    const [sales, setSales] = useState([]);
    const [showForm, setShowForm] = useState(false);
    const [viewItem, setViewItem] = useState(null);
    const [search, setSearch] = useState("");

    useEffect(() => {
        loadSales();
    }, []);

    const loadSales = async () => {
        try {
            const res = await api.get("/sale");
            setSales(res.data);
        } catch (err) {
            console.log(err);
        }
    };

    return (
        <MainLayout>

            {/* HEADER */}
            <div className="erp-header">

    {/* LEFT SIDE */}
    <div className="erp-left">
        <h2>Sales</h2>
        <p>POS billing & invoice management</p>
    </div>

    {/* RIGHT SIDE */}
    <div className="erp-right">

        <div className="erp-search">
            <input
                placeholder="Search..."
                value={search}
                onChange={(e) => setSearch(e.target.value)}
            />
        </div>

        <button className="erp-btn" onClick={() => setShowForm(true)}>
            ➕ Add
        </button>

    </div>

</div>

            {/* <div className="page-header-sale">

                <div className="page-title">
                    <h2>Sales</h2>
                    <p>POS billing & invoice management</p>
                </div>

                <div className="search-box">
                    <input
                        placeholder="Search invoice or customer..."
                        value={search}
                        onChange={(e) => setSearch(e.target.value)}
                    />
                </div>

                <button
                    className="add-btn-sale"
                    onClick={() => setShowForm(true)}
                >
                    ➕ New Sale
                </button>

            </div> */}

            {/* TABLE */}
            <table className="table-sale">

                <thead>
                    <tr>
                        <th>Invoice</th>
                        <th>Customer</th>
                        <th>Date</th>
                        <th>Total</th>
                        <th>Actions</th>
                    </tr>
                </thead>

                <tbody>
                    {sales
                        .filter(s =>
                            s.customerName?.toLowerCase().includes(search.toLowerCase()) ||
                            s.invoiceNo?.toLowerCase().includes(search.toLowerCase())
                        )
                        .map(s => (
                            <tr key={s.id}>
                                <td>{s.invoiceNo}</td>
                                <td>{s.customerName}</td>
                                <td>{new Date(s.saleDate).toLocaleDateString()}</td>
                                <td>{s.totalAmount}</td>

                                <td>
                                    <button
                                        className="view-btn"
                                        onClick={() => setViewItem(s.id)}
                                    >
                                        👁
                                    </button>
                                </td>
                            </tr>
                        ))}
                </tbody>

            </table>

            {/* SALE FORM */}
            {showForm && (
                <SaleForm
                    onClose={() => setShowForm(false)}
                    onSuccess={loadSales}
                />
            )}

            {/* DETAILS */}
            {viewItem && (
                <SaleDetails
                    id={viewItem}
                    onClose={() => setViewItem(null)}
                />
            )}

        </MainLayout>
    );
}