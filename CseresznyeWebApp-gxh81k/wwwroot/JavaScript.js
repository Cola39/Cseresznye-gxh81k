function load() {
    fetch("api/library").then(r => r.json()).then(o => {

        var tbody = document.getElementById("tbody");
        tbody.innerHTML = "";

        o.forEach(book => {
            var row = document.createElement("tr");
            row.innerHTML = `<td>${book.bookId}</td>
                             <td>${book.title}</td>
                             <td>${book.author}</td>
                             <td>${book.genre}</td>
                             <td><img src="" id="cover-${book.bookId}" alt="Not available"></td>`
            tbody.appendChild(row);

            fetch(`https://openlibrary.org/search.json?title=${book.title}`).then(response => response.json()).then(data => {
                if (data.docs && data.docs.length > 1) {
                    var isbn = data.docs[0].isbn ? data.docs[0].isbn[0] : null;
                    if (isbn) {
                        var img = document.getElementById(`cover-${book.bookId}`);
                        img.src = `https://covers.openlibrary.org/b/isbn/${isbn}-M.jpg`
                    }
                }
            })
        });
    });
}

document.addEventListener("DOMContentLoaded", _ => { load(); })

document.getElementById("add").addEventListener("click", _ => {
    var bookData = {
        "title": document.getElementById("titleAdd").value,
        "author": document.getElementById("authorAdd").value,
        "genre": document.getElementById("genreAdd").value
    };

    fetch("api/library", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(bookData)
    }).then(r => {
        if (r.ok) {
            alert("Success");
            load();
            document.getElementById("titleAdd").value = "";
            document.getElementById("authorAdd").value = "";
            document.getElementById("genreAdd").value = "";
        }
        else alert("Fail")
    })
})

document.getElementById("modify").addEventListener("click", _ => {
    var bookData = {
        "bookId": document.getElementById("idMod").value,
        "title": document.getElementById("titleMod").value,
        "author": document.getElementById("authorMod").value,
        "genre": document.getElementById("genreMod").value
    };

    fetch(`api/library/${bookData.bookId}`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(bookData)
    }).then(r => {
        if (r.ok) {
            alert("Success");
            load();
            document.getElementById("idMod").value = "";
            document.getElementById("titleMod").value = "";
            document.getElementById("authorMod").value = "";
            document.getElementById("genreMod").value = "";
        }
        else alert("Fail")
    })
})

document.getElementById("delete").addEventListener("click", _ => {
    var bookData = {
        "bookId": document.getElementById("id").value
    };

    fetch(`api/library/${bookData.bookId}`, {
        method: "DELETE",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(bookData)
    }).then(r => {
        if (r.ok) {
            alert("Success");
            load();
            document.getElementById("id").value = "";
        }
        else alert("Fail")
    })
})