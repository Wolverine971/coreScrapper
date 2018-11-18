<template>
  <v-card
    color="blue-grey white--text"
    dark
  >
    <v-container
    id="input-usage"
    grid-list-xl
    fluid
  >
    <v-flex>
        <v-form v-model="valid">
            <v-text-field
            v-model="URL"
            label="URL"
            @keydown.enter.native="scrape"
            ></v-text-field>
            <v-text-field
            v-model="word"
            label="word"
            ></v-text-field>
            <v-btn @click="scrape">Scrape</v-btn>
            <v-btn @click="clear">clear</v-btn>
 
        </v-form>
      </v-flex>
  </v-container>
  


  </v-card>
</template>

<script>
import axios from 'axios'
import { mapActions, mapState } from 'vuex'

  export default {
    name: 'ScrapeForm',
    data: () => ({
      descriptionLimit: 60,
      entries: [],
      isLoading: false,
      model: null,
      search: null,

      URL: null,
      word: null
    }),
    methods:{
        
        scrape(){
            let scrapeRequest = {
                Website: this.URL,
                SearchingFor: this.word
            }
            this.$store.dispatch('scrape', scrapeRequest);
            // ...mapActions(['scrape'], scrapeRequest)

            // api.scrapeSite(scrapeRequest).then(function(){

            // })
            // var settings = {
            //     url: "http://localhost:60810/" + apiRoute,
            //     method: 'POST',
            //     cache: false,
            //     contentType: "application/json; charset=UTF-8;",
            //     data: JSON.stringify(data)
            // };
        },
        clear(){
          this.URL = "",
          this.word = ""
        }

    },

    computed: {
      fields () {
        if (!this.model) return []

        return Object.keys(this.model).map(key => {
          return {
            key,
            value: this.model[key] || 'n/a'
          }
        })
      },
      items () {
        return this.entries.map(entry => {
          const Description = entry.Description.length > this.descriptionLimit
            ? entry.Description.slice(0, this.descriptionLimit) + '...'
            : entry.Description

          return Object.assign({}, entry, { Description })
        })
      }
    },

    watch: {
      search (val) {
        // Items have already been loaded
        if (this.items.length > 0) return

        // Items have already been requested
        if (this.isLoading) return

        this.isLoading = true

        // Lazily load input items
        axios.get('https://api.publicapis.org/entries')
          .then(res => {
            const { count, entries } = res.data
            this.count = count
            this.entries = entries
          })
          .catch(err => {
            console.log(err)
          })
          .finally(() => (this.isLoading = false))
      }
    }
  }
    

</script>
<style>

</style>
