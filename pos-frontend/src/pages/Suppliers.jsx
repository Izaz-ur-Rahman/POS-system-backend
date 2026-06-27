import { useEffect, useState } from "react";
import MainLayout from "../layouts/MainLayout";
import api from "../api/axios";

import SupplierForm from "../components/SupplierForm";

import "../styles/pages/supplier.css";


export default function Suppliers() {

    const [suppliers, setSuppliers] = useState([]);
    const [showModal, setShowModal] = useState(false);
    const [editData, setEditData] = useState(null);
    const [deleteItem, setDeleteItem] = useState(null);
    const [search, setSearch] = useState("");

    useEffect(() => {
        loadSuppliers();
    }, []);

    const loadSuppliers = async () => {
        try {
            const res = await api.get("/supplier");
            setSuppliers(res.data);
        } catch (err) {
            console.log(err);
        }
    };

    const handleDelete = async (id) => {
        try {
            await api.delete(`/supplier/${id}`);
            setDeleteItem(null);
            loadSuppliers();
        } catch (err) {
            console.log(err);
            alert("Delete failed");
        }
    };

    return (
        <MainLayout>

            {/* HEADER */}
            <div className="page-header-supplier">
                <div className="page-title">
                    <h2>Suppliers</h2>
                    <p>Supplier management module</p>
                </div>
 <div className="search-box">
                <input
                    placeholder="Search supplier..."
                    value={search}
                    onChange={(e) => setSearch(e.target.value)}
                />
            </div>
                <button
                    className="add-btn-supplier"
                    onClick={() => {
                        setEditData(null);
                        setShowModal(true);
                    }}
                >
                    ➕ Add Supplier
                </button>
            </div>

            {/* SEARCH */}
           

            {/* TABLE */}
            <table className="table-supplier">
                <thead>
                    <tr>
                      
                        <th>Name</th>
                        <th>Phone</th>
                        <th>Company</th>
                        <th>Email</th>
                        <th>Actions</th>
                    </tr>
                </thead>

                <tbody>
                    {suppliers
                        .filter(s =>
                            s.name?.toLowerCase().includes(search.toLowerCase()) ||
                            s.phone?.includes(search)
                        )
                        .map(s => (
                            <tr key={s.id}>
                          
                                <td>{s.name}</td>
                                <td>{s.phone}</td>
                                <td>{s.companyName}</td>
                                <td>{s.email}</td>

                                <td>
                                    <div className="actions-supplier">

                                        <button
                                            className="action-btn-s edit-btn-s"
                                            onClick={() => {
                                                setEditData(s);
                                                setShowModal(true);
                                            }}
                                        >
                                            ✏
                                        </button>

                                        <button
                                            className="delete"
                                            onClick={() => setDeleteItem(s)}
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
            <SupplierForm
                show={showModal}
                editData={editData}
                onClose={() => setShowModal(false)}
                onSuccess={loadSuppliers}
            />

            {/* DELETE MODAL */}
            {deleteItem && (
                <div className="delete-overlay">
                    <div className="delete-modal">

                        <h2>Delete Supplier?</h2>

                        <p>
                            Are you sure you want to delete <b>{deleteItem.name}</b>
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
                                onClick={() => handleDelete(deleteItem.id)}
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