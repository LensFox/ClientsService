import * as actions from './actions'

const initialState = {
    clients: []
}

const reducer = (state = initialState, action) => {
    switch(action.type){
        case actions.FETCH_CLIENTS_SUCCEEDED:
            return {
                clients: action.payload
            }

        case actions.DELETE_CLIENT_SUCCEEDED:
            const newClients = state.clients.filter(client => client.id !== action.payload)

            return { clients: newClients }

        default:
            return state;
    }
}

export default reducer;