import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { AppThunk, RootState } from '../../app/store';
import { AxiosResponse } from "axios";
import { showToast, makeAxiosInstance } from '../utils';
import produce from "immer";

interface Group {
    id: number
    name: string
    description: string
    isAdmin: boolean
    users: User[]
}

export interface User {
    id: number
    name: string
}

export type loadingState = 'idle' | 'pending';

interface UserGroupState {
    loading: loadingState,
    currentUser: User,
    groups: Group[],
    error: boolean
}

const initialState: UserGroupState = {
    currentUser: {
        id: 0,
        name: ''
    },
    groups: [],
    loading: 'idle',
    error: false
}

const slice = createSlice({
    name: 'userGroup',
    initialState,
    reducers: {
        error: (state, action: PayloadAction<boolean>) => {
            state.error = action.payload;
        },
        loading: (state) => {
            state.error = false;
            state.loading = 'pending';
        },
        loaded: (state, action: PayloadAction<UserGroupState>) => {
            state.groups = action.payload.groups;
            state.currentUser = action.payload.currentUser;
            state.loading = 'idle';
        },
        create: (state, action: PayloadAction<Group>) => {
            state.groups = state.groups.concat([action.payload]);
        },
        update: (state, action: PayloadAction<{id: number, name: string, description: string}>) => {
            const index = state.groups.findIndex(el => el.id === action.payload.id);
            state.groups[index].name = action.payload.name;
            state.groups[index].description = action.payload.description;
        },
        join: (state, action: PayloadAction<number>) => {
            const index = state.groups.findIndex(el => el.id === action.payload);
            const newState = produce<UserGroupState>(state, draft => 
            {
                draft.groups[index].users.push(state.currentUser)
            });
            state.groups = newState.groups;     
        },
        remove: (state, action: PayloadAction<{groupId: number, userId: number}>) => {
            const index = state.groups.findIndex(el => el.id === action.payload.groupId);
            const newState = produce<UserGroupState>(state, draft => 
            {
                draft.groups[index].users = state.groups[index].users.filter(el => el.id !== action.payload.userId);
            });
            state.groups = newState.groups;
        }
    }
});

const { create, update, join, remove, loading, loaded, error }  = slice.actions;

const axios = makeAxiosInstance();

const getToken = () => localStorage.getItem('token');

export const loadAsync = (): AppThunk => dispatch => { 
    dispatch(loading());  
    axios.get('/usergroups', { headers: { 'Authorization': `Bearer ${getToken()}` } })
    .then((axiosResponse: AxiosResponse<UserGroupState>) => {
        dispatch(loaded(axiosResponse.data));
        showToast('success', `Your groups are now loaded!`)
    })
    .catch(() => {
        showToast('error', `Sorry! Groups could not be loaded`);
        dispatch(error(true));
    });    
};

export const createAsync = (name: string, description: string): AppThunk => dispatch => { 
    axios.post('/groups', { name, description }, { headers: { 'Authorization': `Bearer ${getToken()}` } })
    .then((axiosResponse: AxiosResponse<Group>) => {
        dispatch(create(axiosResponse.data));
        showToast('success', `Your new group ${name} was created!`)
    })
    .catch(() => {
        showToast('error', `Sorry! Your request to create group ${name} failed`)
        dispatch(error(true));
    });    
};
  
export const updateAsync = (id: number, name: string, description: string): AppThunk => dispatch => { 
    axios.put(`/groups/${id}`, { name, description }, { headers: { 'Authorization': `Bearer ${getToken()}` } })
    .then(() => {
        dispatch(update({ id, name, description }));
        showToast('success', `Your group ${name} was updated!`)
    })
    .catch(() => {
        showToast('error', `Sorry! Your request to update group ${name} failed`)
        dispatch(error(true));
    });       
};

export const joinAsync = (groupId: number, groupName: string): AppThunk => dispatch => { 
    axios.post(`usergroups/${groupId}`, {}, { headers: { 'Authorization': `Bearer ${getToken()}` } })
    .then(() => {
        dispatch(join(groupId));
        showToast('success', `You joined ${groupName} group!`)
    })
    .catch(() => {
        showToast('error', `Sorry! Your request to join group ${groupName} failed`)
        dispatch(error(true));
    });
};

export const removeAsync = (userId: number, groupId: number, userName: string, groupName: string): AppThunk => dispatch => { 
    const body = { groupId: groupId, userId: userId };
    axios.delete(`/usergroups/users/${userId}/groups/${groupId}`, { headers: { 'Authorization': `Bearer ${getToken()}` } })
    .then(() => {
        dispatch(remove(body));
        showToast('success', `You removed ${userName} from ${groupName} group!`)
    })
    .catch(() => {
        showToast('error', `Sorry! Your request to remove user ${userName} from group ${groupName} failed`)
        dispatch(error(true));
    });
};

export const selectLoadingStatus = (state: RootState) => state.userGroups.loading;

export const selectGroups = (state: RootState) => state.userGroups.groups;

export const selectCurrentUser = (state: RootState) => state.userGroups.currentUser;

export const selectError = (state: RootState) => state.userGroups.error;

export default slice.reducer
