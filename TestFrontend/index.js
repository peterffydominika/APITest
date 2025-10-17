document.getElementById("button1").onclick = async () =>
    {
        var url = "http://localhost:5053/books";
    
        var request = await fetch(url, {
            headers: { 'Content-Type': 'application/json' },
            method: 'GET'
        });
    
        if (!request.ok)
        {
            console.log("Hiba")
            return;
        }
        var response = await request.json();
        console.log(response);
    }