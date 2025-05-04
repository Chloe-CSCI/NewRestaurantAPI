"use strict";
// this will be able to show what is in the index for the customerFoodIndex.
import { FetchRepository } from "./FetchRepository.js";

(async function _CustomerFoodIndex(){
    const repo = new FetchRepository("/api/customerfoodapi");
    repo.readAllAPIName = "customerfoodcombo";

    try {
        const dish = await repo.readAll();
        populateTable(dish);
    }
    catch(error) {
        console.error('Error:', error)
    }
})();


//This will help populate the dish.
function populateTable(dish) {
    const tableBody = document.getElementById("tableBody");
    dish.forEach(item) => {
        const tr = document.getElementById("tr");
        for (let i in item) {
            const td = document.getElementById("td")
            let text = item[i];
            if (text === '' && i === 'menuItem') {
                text = "there is no meal";
            }

            let textNode = document.createTextNode(text);
            td.appendChild(textNode);
            tr.appendChild(td);
        }
        tableBody.appendChild(tr);
    });
}