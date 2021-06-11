import { CLIENTS_API_PATH } from "./constants";
import axios from 'axios'

export const FETCH_CLIENTS_SUCCEEDED = 'FETCH_CLIENTS_SUCCEEDED';
export function fetchClients() {
    return async (dispatch) => {
        const response = await axios.get(CLIENTS_API_PATH);

        dispatch({
            type: FETCH_CLIENTS_SUCCEEDED,
            payload: response.data
        });
    }
}

export const DELETE_CLIENT_SUCCEEDED = 'DELETE_CLIENT_SUCCEEDED';
export function deleteClient(id) {
    return async (dispatch) => {
        await axios.delete(`${CLIENTS_API_PATH}/${id}`);

        dispatch({
            type: DELETE_CLIENT_SUCCEEDED,
            payload: id
        })
    }
}

export function addClient(client){
    return async (dispatch) => {
        await axios.post(CLIENTS_API_PATH, client);

        dispatch(fetchClients())
    }
}