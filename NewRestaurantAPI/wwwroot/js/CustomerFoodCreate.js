"use strict";

import { FetchRepository } from "./FetchRepository.js";
//this will help in creating the list for createFood.

(function _customerFoodCreate() {
    const createCustomerFood =
        document.querySelector("#createCustomerFood");
    createCustomerFood.addEventListener('submit', async e => {
        e.preventDefault();
        const repo = new FetchRepository("/api/customerfoodapi");//it will reference back to the customerFoodApi.
        const formData = new FormData(createCustomerFood);
        try {
            const outPut = await repo.create(formData);
            console.log("Success");
            window.location.replace(`/customer/details/${outPut.customerId}`);
        }
        catch (error) {
            console.error('Error:', error);
        }
    });
}) ();