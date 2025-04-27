"use strict";

import { FetchRepository } from "./FetchRepository.js";


(function _customerFoodCreate() {
    const createCustomerFood =
        document.querySelector("#createCustomerFood");
    createCustomerFood.addEventListener('submit', async e => {
        e.preventDefault();
        const repo = new FetchRepository("/api/customerfoodapi");
        const formData = new FormData(createCustomerFood);
        try {
            const outPut = await repo.create(formData);
            console.log("Success");
            window.location.replace(`/customer/details/${result.outPut}`);
        }
        catch (error) {
            console.error('Error:', error);
        }
    });
}) ();