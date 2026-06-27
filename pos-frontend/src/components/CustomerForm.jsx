import { useEffect, useState } from "react";
import api from "../api/axios";

import "../styles/pages/customerModal.css";

export default function CustomerForm({

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
            await api.put(`/customer/${editData.id}`, form);
        } else {
            await api.post("/customer", form);
        }

        onSuccess();
        onClose();
    };

    if (!show) return null;

    return (

        <div className="modal-overlay">

            <div className="customer-modal">

                <h2>
                    {editData ? "✏ Edit Customer" : "➕ Add Customer"}
                </h2>

                <form onSubmit={submit}>

                    <input
                        name="name"
                        placeholder="Name"
                        value={form.name}
                        onChange={handleChange}
                        required
                    />

                    <input
                        name="phone"
                        placeholder="Phone (POS KEY)"
                        value={form.phone}
                        onChange={handleChange}
                        required
                    />

                    <input
                        name="email"
                        placeholder="Email"
                        value={form.email}
                        onChange={handleChange}
                    />

                    <input
                        name="address"
                        placeholder="Address"
                        value={form.address}
                        onChange={handleChange}
                    />

                    <div className="btns">

                        <button type="submit" className="save">
                            Save
                        </button>

                        <button type="button" className="cancel" onClick={onClose}>
                            Cancel
                        </button>

                    </div>

                </form>

            </div>

        </div>

    );

}