import { useEffect, useState } from "react";
import MainLayout from "../layouts/MainLayout";
import api from "../api/axios";
import PurchaseForm from "../components/PurchaseForm";
import PurchaseDetails from "../components/PurchaseDetails";
import "../styles/pages/purchase.css";

export default function Purchase() {

    const [purchases, setPurchases] = useState([]);
    const [showForm, setShowForm] = useState(false);
    const [viewItem, setViewItem] = useState(null);
    const [search, setSearch] = useState("");

    useEffect(() => {
        loadPurchases();
    }, []);

    const loadPurchases = async () => {
        try {
            const res = await api.get("/purchase");
            setPurchases(res.data);
        } catch (err) {
            console.log(err);
        }
    };

    return (
        <MainLayout>

            {/* HEADER */}
            {/* <div className="page-header-purchase">

                <div className="page-title">
                    <h2>Purchases</h2>
                    <p>Supplier purchase & stock management</p>
                </div>
 
               <div className="search-box">
                <input
                    placeholder="Search purchase..."
                    value={search}
                    onChange={(e) => setSearch(e.target.value)}
                />
              </div>
                <button
                    className="add-btn-purchase"
                    onClick={() => setShowForm(true)}
                >
                    ➕ Add Purchase
                </button>

            </div> */}

         <div className="erp-header">

    {/* LEFT SIDE */}
    <div className="erp-left">
        <h2>Purchases</h2>
        <p>Supplier purchase & stock management</p>
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

            {/* TABLE */}
            <table className="table-purchase">

                <thead>
                    <tr>
                      
                        <th>Supplier</th>
                        <th>Date</th>
                        <th>Total</th>
                        <th>Actions</th>
                    </tr>
                </thead>

                <tbody>
                    {purchases
                        .filter(p =>
                            p.supplierName?.toLowerCase().includes(search.toLowerCase()) ||
                            p.purchaseNo?.toLowerCase().includes(search.toLowerCase())
                        )
                        .map(p => (
                            <tr key={p.id}>
                             
                                <td>{p.supplierName}</td>
                                <td>{new Date(p.purchaseDate).toLocaleDateString()}</td>
                                <td>{p.totalAmount}</td>

                                <td>
                                    <button
                                        className="view-btn"
                                        onClick={() => setViewItem(p.id)}
                                    >
                                        👁
                                    </button>
                                </td>
                            </tr>
                        ))}
                </tbody>

            </table>

            {/* FORM MODAL */}
            {showForm && (
                <PurchaseForm
                    onClose={() => setShowForm(false)}
                    onSuccess={loadPurchases}
                />
            )}

            {/* DETAILS MODAL */}
            {viewItem && (
                <PurchaseDetails
                    id={viewItem}
                    onClose={() => setViewItem(null)}
                />
            )}

        </MainLayout>
    );
}