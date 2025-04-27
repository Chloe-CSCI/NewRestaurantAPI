"using strict";

export class FetchRepository {
    #baseURL
    #deleteAPIName
    #readAllAPIName
    #updateAPIName
    constructor(baseURL) {
        this.#baseURL = baseURL;
        this.#readAllAPIName = "all";
        this.#deleteAPIName = "delete";
        this.#updateAPIName = "update";

    }

    set deleteAPIName(name) {
        this.#deleteAPIName = name;
    }

    set updateAPIName(name) {
        this.#updateAPIName = name;
    }

    set readAllAPIName(name) {
        this.#readAllAPIName = name;
    }

    async readAll() {
        const address = `${this.#baseURL}/${this.#readAllAPIName}`;
        const response = await fetch(address);
        if (!response.ok) {
            throw new Error("There was an HTTP error getting the data.");
        }
        return await response.json();
    }

    async read(id) {
        const address = `${this.#baseURL}/one/${id}`;
        const response = await fetch(address);
        if (!response.ok) {
            throw new Error("There was an HTTP error getting the data.");
        }
        return await response.json();
    }

    async create(formData) {
        const address = `${this.#baseURL}/create`;
        const response = await fetch(address, {
            method: "post",
            body: formData
        });
        if (!response.ok) {
            throw new Error("There was an HTTP error creating the data.");
        }
        return await response.json();
    }

    async update(formData) {
        const address = `${this.#baseURL}/${this.#updateAPIName}`;
        const response = await fetch(address, {
            method: "put",
            body: formData
        });
        if (!response.ok) {
            throw new Error("There was an HTTP error updating the data.");
        }
        return await response.text();
    }

    async delete(formData) {
        const address = `${this.#baseURL}/${this.#deleteAPIName}`;
        const response = await fetch(address, {
            method: "delete",
            body: formData
        });
        if (!response.ok) {
            throw new Error("There was an HTTP error deleting the pet data.");
        }
        return await response.text();
    }


}