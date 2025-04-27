"use strict";

import { FetchRepository } from "./FetchRepository.js";


(function _customerFoodCustomerMeal() {
    const formCustomerMeal =
        document.querySelector("#formCustomerMeal");
    formCustomerMeal.addEventListener('submit', async e => {
        e.preventDefault();
        const repo = new FetchRepository("/api/customerfoodapi");
        repo.deleteAPIName = "CustomerMeal";
        const formData = new FormData(formCustomerMeal);
        const Id = formData.get("Id");
        try {
            await repo.update(formData);
            console.log("success");
            window.location.replace(`/customer/details/${Id}`);
        }
        catch (error) {
            console.error('Error:', error);
        }
    });

})();