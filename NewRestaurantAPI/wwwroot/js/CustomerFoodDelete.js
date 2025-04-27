"use strict";

import { FetchRepository } from "./FetchRepository.js";

(function _customerFoodDelete() {
    const deleteForm =
        document.querySelector("#deleteForm");
    deleteForm.addEventListener('submit', async e => {
        e.preventDefault();
        const repo = new FetchRepository("/api/customerfoodapi");
        repo.deleteAPIName = "Delete";
        const formData = new FormData(deleteForm);
        const Id = formData.get("Id");
        try {
            await repo.delete(formData);
            console.log("The Customer and their meal were deleted");
            window.location.replace(`/customer/details/${Id}`);
        }
        catch (error) {
            console.error('Error:', error);

        }
    });
})();