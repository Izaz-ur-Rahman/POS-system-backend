import MainLayout from "../layouts/MainLayout";
import { useEffect, useState } from "react";
import api, { API_BASE_URL } from "../api/axios";
import  "../styles/productModal.css"
import  "../styles/table.css"


import ProductForm from "../components/ProductForm";

export default function Products() {
  const [products, setProducts] = useState([]);
  const [showForm, setShowForm] = useState(false);
  const [editData, setEditData] = useState(null);
  const [viewData, setViewData] = useState(null);
  const [deleteItem, setDeleteItem] = useState(null);
    const [search, setSearch] = useState("");

  useEffect(() => {
    loadProducts();
  }, []);

  const loadProducts = async () => {
    try {
      const res = await api.get("/product");
      setProducts(res.data);
    } catch (err) {
      console.log("Load error:", err);
    }
  };

  return (
    <MainLayout>
      {/* HEADER */}
      {/* <div className="table-header">
        <h2>Products</h2>

     <button className="add-product-btn compact"
  onClick={() => {
    setEditData(null);
    setShowForm(true);
  }}
>
  ➕ Add Product
</button>
      </div> */}
<div className="page-header">

  {/* LEFT SIDE */}
  <div className="page-title">
    <h2>Products</h2>
    <p>Manage your inventory, stock & pricing</p>
  </div>

  {/* RIGHT SIDE */}
  <div className="page-actions">

    {/* SEARCH */}
    <div className="search-box">
     <input
  type="text"
  placeholder="Search products..."
  value={search}
  onChange={(e) => setSearch(e.target.value)}
/>
    </div>

    {/* ADD BUTTON */}
    <button
      className="add-product-btn"
      onClick={() => {
        setEditData(null);
        setShowForm(true);
      }}
    >
      ➕ Add Product
    </button>

  </div>

</div>
      {/* TABLE */}
      <table>
        <thead>
          <tr>
        
            <th>Image</th>
            <th>Name</th>
            <th>Stock</th>
            <th>Price</th>
            <th>Actions</th>
          </tr>
        </thead>

        <tbody>
          {products
            .filter(p =>
                            p.name?.toLowerCase().includes(search.toLowerCase()) 
                        )
                        .map((p) => (
            <tr key={p.id}>
             

              {/* IMAGE */}
              <td>
                <img
                  src={
                    p.imagePath
                      ? `${API_BASE_URL}${p.imagePath}`
                      : "https://via.placeholder.com/50"
                  }
                  alt={p.name}
                  style={{
                    width: 45,
                    height: 45,
                    objectFit: "cover",
                    borderRadius: 8,
                    border: "1px solid #ddd",
                  }}
                />
              </td>

              <td>{p.name}</td>
              <td>{p.stockQuantity}</td>
              <td>{p.salePrice}</td>

              {/* ACTIONS */}
              <td style={{ display: "flex", gap: "6px" }}>

                {/* VIEW */}
                <button
                  onClick={() => setViewData(p)}
                  style={{ background: "rgb(57 77 110)", color: "white" }}
                >
                  👁
                </button>

                {/* EDIT */}
                <button
                  onClick={() => {
                    setEditData(p);
                    setShowForm(true);
                  }}
                  style={{ background: "rgb(57 77 110)", color: "white" }}
                >
                  ✏
                </button>

                {/* DELETE */}
                <button
  className="icon delete"
  onClick={() => setDeleteItem(p)}
>
  🗑
</button>

              </td>
            </tr>
          ))}
        </tbody>
      </table>

      {/* PRODUCT FORM MODAL */}
      <ProductForm
        show={showForm}
        editData={editData}
        onClose={() => setShowForm(false)}
        onSuccess={loadProducts}
      />

      {/* VIEW MODAL */}
      {viewData && (
        <div className="modal-overlay">
          <div className="modal-box">

            <h2>Product Details</h2>

            <img
              src={
                viewData.imagePath
                  ? `${API_BASE_URL}${viewData.imagePath}`
                  : "https://via.placeholder.com/120"
              }
              alt={viewData.name}
              style={{
                width: 120,
                height: 120,
                objectFit: "cover",
                borderRadius: 10,
                marginBottom: 15,
              }}
            />

            <p><b>Name:</b> {viewData.name}</p>
            <p><b>Barcode:</b> {viewData.barcode}</p>
            <p><b>Stock:</b> {viewData.stockQuantity}</p>
            <p><b>Price:</b> {viewData.salePrice}</p>
            <p><b>Category:</b> {viewData.categoryName}</p>

            <button
              onClick={() => setViewData(null)}
              style={{
                marginTop: 10,
                padding: "8px 12px",
                background: "#6b7280",
                color: "white",
                border: "none",
                borderRadius: 6,
                cursor: "pointer"
              }}
            >
              Close
            </button>

          </div>
        </div>
      )}
{deleteItem && (
  <div className="modal-overlay">

    <div className="modal-box delete-modal">

      <div className="delete-icon">⚠️</div>

      <h2>Delete Product?</h2>

      <p>
        Are you sure you want to delete <b>{deleteItem.name}</b>?
        <br />
        This action cannot be undone.
      </p>

      <div className="delete-actions">

        <button
          className="btn-cancel"
          onClick={() => setDeleteItem(null)}
        >
          Cancel
        </button>

        <button
          className="btn-delete"
          onClick={async () => {
            try {
              await api.delete(`/product/${deleteItem.id}`);
              setDeleteItem(null);
              loadProducts();
            } catch (err) {
              console.log(err);
            }
          }}
        >
          Yes, Delete
        </button>

      </div>

    </div>

  </div>
)}
    </MainLayout>
  );
}