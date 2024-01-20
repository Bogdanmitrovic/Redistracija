const baseUrl = "http://localhost:5200/Rampa";

async function kreirajTag(){
    var tag ={
        "id": document.getElementById("01tagIdInput").value,
        "registracija": document.getElementById("01registracijaInput").value
    }
    fetch(baseUrl + "/KreirajTag", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(tag)
    }).then(async resp=>{
        if(resp.ok){
            alert("Uspesno kreiran tag");
        }
        else{
            alert(await resp.text())
        }
    })
}

async function getKredit(){
    fetch(baseUrl + "/GetKredit?tagId="+document.getElementById("02tagIdInput").value, {
        method: "GET",
        headers: {
            "Content-Type": "application/json"
        }
    }).then(async resp=>{
        if(resp.ok){
            alert(await resp.json());
        }
        else{
            alert(await resp.text())
        }
    })
}

async function getTag(){
    fetch(baseUrl + "/GetTag?tagId="+document.getElementById("03tagIdInput").value, {
        method: "GET",
        headers: {
            "Content-Type": "application/json"
        }
    }).then(async resp=>{
        if(resp.ok){
            alert(await JSON.stringify(await resp.text()));
        }
        else{
            alert(await resp.text())
        }
    })
}

async function uplatiKredit(){
    var tag ={
        "id": document.getElementById("04tagIdInput").value,
        "kredit": parseInt(document.getElementById("04kreditInput").value)
    }
    fetch(baseUrl + "/UplatiKredit", {
        method: "PUT",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(tag)
    }).then(async resp=>{
        if(resp.ok){
            alert("Uspesno uplaceno");
        }
        else{
            alert(await resp.text())
        }
    })
}

async function zameniRegistraciju(){
    fetch(baseUrl + "/ZameniRegistraciju?tagId="+document.getElementById("05tagIdInput").value+"&novaRegistracija="+document.getElementById("05registracijaInput").value, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json"
        }
    }).then(async resp=>{
        if(resp.ok){
            alert("Uspesno zamenjena registracija");
        }
        else{
            alert(await resp.text())
        }
    })
}

async function obrisiTag(){
    fetch(baseUrl + "/ObrisiTag?tagId="+document.getElementById("06tagIdInput").value, {
        method: "DELETE",
        headers: {
            "Content-Type": "application/json"
        }
    }).then(async resp=>{
        if(resp.ok){
            alert("Uspesno obrisan tag");
        }
        else{
            alert(await resp.text())
        }
    })
}

async function otvoriRampu(){
    fetch(baseUrl + "/OtvoriRampu?tagId="+document.getElementById("01tagIdInput").value, {
        method: "GET",
        headers: {
            "Content-Type": "application/json"
        }
    }).then(async resp=>{
        if(resp.ok){
            alert("Rampa otvorena");
        }
        else{
            alert(await resp.text())
        }
    })

}

async function otvoriRampuPoRegistraciji(){
    fetch(baseUrl + "/OtvoriRampuPoRegistraciji?registracija="+document.getElementById("02registracijaInput").value, {
        method: "GET",
        headers: {
            "Content-Type": "application/json"
        }
    }).then(async resp=>{
        if(resp.ok){
            alert("Rampa otvorena");
        }
        else{
            alert(await resp.text())
        }
    })

}

async function nadjiTagIdPoRegistraciji(){
    fetch(baseUrl + "/NadjiTagIdPoRegistraciji?registracija="+document.getElementById("03registracijaInput").value, {
        method: "GET",
        headers: {
            "Content-Type": "application/json"
        }
    }).then(async resp=>{
        if(resp.ok){
            alert(await resp.text());
        }
        else{
            alert(await resp.text())
        }
    })

}

async function skiniKreditPoRegistraciji(){
    fetch(baseUrl + "/SkiniKreditPoRegistraciji?registracija="+document.getElementById("04registracijaInput").value+"&kredit="+document.getElementById("04kreditInput").value, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json"
        }
    }).then(async resp=>{
        if(resp.ok){
            alert("Uspesno skinut kredit");
        }
        else{
            alert(await resp.text())
        }
    })

}

async function skiniKredit(){
    tag={
        "id": document.getElementById("05tagIdInput").value,
        "kredit": parseInt(document.getElementById("05kreditInput").value)
    }
    fetch(baseUrl + "/SkiniKredit", {
        method: "PUT",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(tag)
    }).then(async resp=>{
        if(resp.ok){
            alert("Uspesno skinut kredit");
        }
        else{
            alert(await resp.text())
        }
    })

}