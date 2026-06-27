import api, { API_BASE_URL } from "../api/axios";

export default function ProductViewModal({ show, product, onClose }) {
  if (!show || !product) return null;

  return (
    <div className="modal-overlay">
      <div className="modal-box view-modal">

        {/* HEADER */}
        <div className="modal-header">
          <h2>📦 Product Details</h2>
          <button className="close-btn" onClick={onClose}>✕</button>
        </div>

        {/* BODY */}
        <div className="view-content">

          {/* IMAGE */}
          <div className="view-image">
            {product.imagePath ? (
              <img
                src={`${API_BASE_URL}${product.imagePath}`}
                alt="product"
              />
            ) : (
              <div className="no-image">No Image</div>
            )}
          </div>

          {/* INFO */}
          <div className="view-info">

            <div className="info-row">
              <span>Name</span>
              <b>{product.name}</b>
            </div>

            <div className="info-row">
              <span>Barcode</span>
              <b>{product.barcode || "-"}</b>
            </div>

            <div className="info-row">
              <span>Sale Price</span>
              <b>Rs {product.salePrice}</b>
            </div>

            <div className="info-row">
              <span>Purchase Price</span>
              <b>Rs {product.purchasePrice}</b>
            </div>

            <div className="info-row">
              <span>Stock</span>
              <b>{product.stockQuantity}</b>
            </div>

            <div className="info-row">
              <span>Min Stock</span>
              <b>{product.minStockLevel}</b>
            </div>

            <div className="info-row">
              <span>Category</span>
              <b>{product.categoryName}</b>
            </div>

            <div className="info-row">
              <span>Status</span>
              <b className={product.isActive ? "active" : "inactive"}>
                {product.isActive ? "Active" : "Inactive"}
              </b>
            </div>

          </div>

        </div>

        {/* FOOTER */}
        <div className="modal-footer">
          <button onClick={onClose} className="btn-close">
            Close
          </button>
        </div>

      </div>
    </div>
  );
}