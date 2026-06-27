import api from "../../api/axios";

export default function PrintInvoice({ saleId }) {

    const printInvoice = async () => {

        const res = await api.get(`/invoice/${saleId}/print`);

        const data = res.data;

        const win = window.open("", "", "width=400,height=600");

        win.document.write(`
            <html>
            <body>

                <h2>${data.store}</h2>

                <hr/>

                <p>Invoice : ${data.invoiceNo}</p>
                <p>Customer : ${data.customerName}</p>

                <hr/>

                ${data.items.map(x => `
                    <p>
                        ${x.productName}
                        ${x.quantity} x ${x.price}
                    </p>
                `).join("")}

                <hr/>

                <h3>Total : ${data.totalAmount}</h3>

            </body>
            </html>
        `);

        win.document.close();

        win.print();
    };

    return (
        <button onClick={printInvoice}>
            🖨 Print Invoice
        </button>
    );
}