import { useEffect, useState } from "react";
import MainLayout from "../layouts/MainLayout";
import api from "../api/axios";

import CustomerForm from "../components/CustomerForm";

import "../styles/pages/customer.css";

export default function Customers() {

    const [customers, setCustomers] = useState([]);

    const [showModal, setShowModal] = useState(false);

    const [editData, setEditData] = useState(null);

    const [deleteItem, setDeleteItem] = useState(null);

    const [search, setSearch] = useState("");

    useEffect(() => {
        loadCustomers();
    }, []);

    const loadCustomers = async () => {

        try {

            const res = await api.get("/customer");

            setCustomers(res.data);

        } catch (err) {
            console.log(err);
        }

    };

    return (

        <MainLayout>

            {/* HEADER */}
            <div className="page-header">

                <div className="page-title">
                    <h2>Customers</h2>
                    <p>POS customer management</p>
                </div>
             {/* SEARCH BAR (PHONE BASED LIKE POS) */}
              <div className="search-box">

                <input
                    placeholder="Search by phone or name..."
                    value={search}
                    onChange={(e) => setSearch(e.target.value)}
                />

              </div>

                <button
                    className="add-btn"
                    onClick={() => {
                        setEditData(null);
                        setShowModal(true);
                    }}
                >
                    ➕ Add Customer
                </button>

            </div>

           

            {/* TABLE */}
            <table className="table-customer">

                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Name</th>
                        <th>Phone</th>
                        <th>Email</th>
                        <th>Credit</th>
                        <th>Status</th>
                        <th>Actions</th>
                    </tr>
                </thead>

                <tbody>

                    {customers
                        .filter(c =>
                            c.name?.toLowerCase().includes(search.toLowerCase()) ||
                            c.phone?.includes(search)
                        )
                        .map(c => (

                            <tr key={c.id}>

                                <td>{c.id}</td>
                                <td>{c.name}</td>
                                <td>{c.phone}</td>
                                <td>{c.email}</td>

                                <td>
                                    <span className={c.creditBalance > 0 ? "credit" : "no-credit"}>
                                        {c.creditBalance}
                                    </span>
                                </td>

                                <td>
                                    <span className={c.isActive ? "active" : "inactive"}>
                                        {c.isActive ? "Active" : "Inactive"}
                                    </span>
                                </td>

                                <td>

                                    <div className="actions">

                                        <button
                                            className="edit"
                                            onClick={() => {
                                                setEditData(c);
                                                setShowModal(true);
                                            }}
                                        >
                                            ✏
                                        </button>

                                        <button
                                            className="delete"
                                            onClick={() => setDeleteItem(c)}
                                        >
                                            🗑
                                        </button>

                                    </div>

                                </td>

                            </tr>

                        ))}

                </tbody>

            </table>

            {/* FORM MODAL */}
            <CustomerForm
                show={showModal}
                editData={editData}
                onClose={() => setShowModal(false)}
                onSuccess={loadCustomers}
            />

            {/* DELETE MODAL */}
            {deleteItem && (

                <div className="delete-overlay">

                    <div className="delete-modal">

                        <div className="icon">⚠️</div>

                        <h2>Delete Customer?</h2>

                        <p>
                            Are you sure you want to delete
                            <br />
                            <b>{deleteItem.name}</b>
                        </p>

                        <div className="btns">

                            <button
                                className="cancel"
                                onClick={() => setDeleteItem(null)}
                            >
                                Cancel
                            </button>

                            <button
                                className="delete"
                                onClick={async () => {

                                    await api.delete(`/customer/${deleteItem.id}`);

                                    setDeleteItem(null);

                                    loadCustomers();

                                }}
                            >
                                Delete
                            </button>

                        </div>

                    </div>

                </div>

            )}

        </MainLayout>

    );

}