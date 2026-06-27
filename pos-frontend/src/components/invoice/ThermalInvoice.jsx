import api from "../../api/axios";

export default function ThermalInvoice({ saleId }) {

    const printThermal = async () => {

        const res = await api.get(`/invoice/${saleId}/print`);

        const data = res.data;

        const receipt = `
<html>

<head>

<title>Receipt</title>

<style>

body{
    width:300px;
    margin:auto;
    font-family:monospace;
    font-size:14px;
}

h2{
    text-align:center;
}

hr{
    border-top:1px dashed black;
}

.row{
    display:flex;
    justify-content:space-between;
}

.center{
    text-align:center;
}

</style>

</head>

<body>

<h2>${data.store}</h2>

<div class="center">
Invoice : ${data.invoiceNo}
<br/>
Customer : ${data.customerName}
<br/>
${new Date(data.date).toLocaleString()}
</div>

<hr/>

${data.items.map(x=>`

<div class="row">
<span>${x.productName}</span>
<span>${x.quantity} x ${x.price}</span>
</div>

`).join("")}

<hr/>

<div class="row">
<span>Subtotal</span>
<span>${data.subTotal}</span>
</div>

<div class="row">
<span>Discount</span>
<span>${data.discount}</span>
</div>

<div class="row">
<span>Tax</span>
<span>${data.tax}%</span>
</div>

<hr/>

<div class="row">
<b>Total</b>
<b>${data.totalAmount}</b>
</div>

<br/>

<div class="center">
Thank You
<br/>
Visit Again
</div>

</body>

</html>
`;

        const win = window.open("", "", "width=350,height=700");

        win.document.write(receipt);

        win.document.close();

        win.focus();

        win.print();

        win.close();
    };

    return (
        <button
            className="thermal-btn"
            onClick={printThermal}
        >
            🧾 Thermal Print
        </button>
    );
}