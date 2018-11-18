import Vue from 'vue'
import Vuex from 'vuex'
import api from '../api/generic.js'

Vue.use(Vuex)

// TYPES
const MAIN_SET_COUNTER = 'MAIN_SET_COUNTER'
const SET_RESULTS = 'SET_RESULTS'

// STATE
const state = {
  counter: 1,
  scrapeResults: null,

}

// MUTATIONS
const mutations = {
  [MAIN_SET_COUNTER] (state, obj) {
    state.counter = obj.counter
  },
  [SET_RESULTS](state, obj){
    state.scrapeResults = obj;
  }

}

// ACTIONS
const actions = ({
  setCounter ({ commit }, obj) {
    commit(MAIN_SET_COUNTER, obj)
  },
  scrape({state, getters, commit, dispatch }, scrapeRequest){
    api.scrapeSite(scrapeRequest)
    .then(data => {
      commit(SET_RESULTS, data);
    })
    .catch((error) => console.log(error))
  }
})

export default new Vuex.Store({
  state,
  mutations,
  actions
})
