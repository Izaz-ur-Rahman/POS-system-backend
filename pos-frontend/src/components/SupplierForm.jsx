import { useEffect, useState } from "react";
import api from "../api/axios";

import "../styles/pages/supplier.css";

export default function SupplierForm({
    show,
    onClose,
    onSuccess,
    editData
}) {

    const [form, setForm] = useState({
        name: "",
        phone: "",
        email: "",
        address: "",
        companyName: "",
        isActive: true
    });

    useEffect(() => {
        if (editData) {
            setForm(editData);
        } else {
            setForm({
                name: "",
                phone: "",
                email: "",
                address: "",
                companyName: "",
                isActive: true
            });
        }
    }, [editData, show]);

    const handleChange = (e) => {
        setForm({ ...form, [e.target.name]: e.target.value });
    };

    const submit = async (e) => {
        e.preventDefault();

        if (editData) {
            await api.put(`/supplier/${editData.id}`, form);
        } else {
            await api.post("/supplier", form);
        }

        onSuccess();
        onClose();
    };

    if (!show) return null;

    return (
        <div className="modal-overlay">
            <div className="supplier-modal">

                <h2>
                    {editData ? "✏ Edit Supplier" : "➕ Add Supplier"}
                </h2>

                <form onSubmit={submit}>

                    <input name="name" placeholder="Name" onChange={handleChange} value={form.name} required />
                    <input name="phone" placeholder="Phone" onChange={handleChange} value={form.phone} required />
                    <input name="email" placeholder="Email" onChange={handleChange} value={form.email} />
                    <input name="address" placeholder="Address" onChange={handleChange} value={form.address} />
                    <input name="companyName" placeholder="Company Name" onChange={handleChange} value={form.companyName} />

                    <div className="btns">
                        <button className="save" type="submit">Save</button>
                        <button className="cancel" type="button" onClick={onClose}>Cancel</button>
                    </div>

                </form>

            </div>
        </div>
    );
}