import reducer from '../reducer';
import * as actions from '../actions';

describe('when reduce clients', () => {
    const clients = [
        {
            id: '1',
            name: 'Client 1'
        },
        {
            id: '2',
            name: 'Client 2'
        },
        {
            id: '3',
            name: 'Client 3'
        },
    ];
    describe('when fetch clients', () => {
        const state = undefined;
        const action = {
            type: actions.FETCH_CLIENTS_SUCCEEDED,
            payload: clients
        };

        const expectedRetult = clients;
        const actualResult = reducer(state, action).clients;

        it('should store all clients', () => {
            expect(actualResult).toEqual(expectedRetult);
        })
    })

    describe('when fetch clients and clients are exist', () => {
        const state = {
            clients: [
                {
                    id: '0',
                    name: 'Client 0'
                }
            ]
        };
        const action = {
            type: actions.FETCH_CLIENTS_SUCCEEDED,
            payload: clients
        };

        const expectedRetult = clients;
        const actualResult = reducer(state, action).clients;

        it('should rewrite clients', () => {
            expect(actualResult).toEqual(expectedRetult);
        })
    })

    describe('when delete existing client', () => {
        const state = {
            clients
        };
        const action = {
            type: actions.FETCH_CLIENTS_SUCCEEDED,
            payload: '1'
        };

        const expectedRetult = [
            clients[1],
            clients[2]
        ];
        const actualResult = reducer(state, action).clients;

        it('should successfully remove first client', () => {
            expect(actualResult).toEqual(expectedRetult);
        })
    })

    describe('when delete a client which is not exist', () => {
        const state = {
            clients
        };
        const action = {
            type: actions.FETCH_CLIENTS_SUCCEEDED,
            payload: '0'
        };

        const expectedRetult = clients;
        const actualResult = reducer(state, action).clients;

        it('state unchanged', () => {
            expect(actualResult).toEqual(expectedRetult);
        })
    })
})

describe('1', () => {
    it('2', () => {
        expect(1 + 1).toEqual(2)
    })
})