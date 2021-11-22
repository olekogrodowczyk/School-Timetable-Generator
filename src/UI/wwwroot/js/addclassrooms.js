var classroomId = 0;
var clasroomsList = [];
class Classroom {

    constructor(kod, nazwa, ilosc_miejsc) {
        this.id = classroomId++;
        this.kod = kod;
        this.nazwa = nazwa;
        this.ilosc_miejsc = ilosc_miejsc;
    }


    get Kod() {
        return this.kod;
    }
    get Nazwa() {
        return this.nazwa;
    }
    get Ilosc_miejsc() {
        return this.ilosc_miejsc;
    }
}

class UI {
    static displayClassroom() {
        var tmp = new Classroom("czyt", "czytelnia", "30");

        UI.addClassRoomToList(tmp);
    }

    static addClassRoomToList(klasa) {

        const list2 = document.querySelector('.all');
        const row2 = document.createElement('li');

        row2.innerHTML = `

            <ul class="item">
             <li> <p>${klasa.kod}</p></li>
             <li> <p>${klasa.nazwa}</p></li>
             <li> <p>${klasa.ilosc_miejsc}</p> </li>
             <li> <div class="btn-container">
              <input class="btn btn-dark" type="submit" value="USUŃ">
            
                   </div>
            </li>
            </ul>
        `;

        list2.appendChild(row2);
    }
    static clearFields() {
        document.querySelector('#kod').placeholder = 'kod';
        document.querySelector('#name').placeholder = 'nazwa';
        document.querySelector('#count').placeholder = '1';

    }
}


var addNew = document.querySelector('#submitNewClassroom');
addNew.addEventListener('click', (e) => {
    e.preventDefault();

    const kod = document.querySelector('#kod').value;
    const nazwa = document.querySelector('#name').value;
    const ilosc_miejsc = document.querySelector('#count').value;

    klasa = new Classroom(kod, nazwa, ilosc_miejsc);

    clasroomsList.push(klasa);

    UI.addClassRoomToList(klasa);

    UI.clearFields();

    localStorage.setItem('MyClassrooms', JSON.stringify(clasroomsList));
});