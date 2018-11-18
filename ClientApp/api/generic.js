import axios from 'axios'

export default {
    server: 'me',
    scrapeRoute: "/api/scrape/site",

    scrapeSite(data){
        let request = {
            Website: data.Website,
            SearchingFor: data.SearchingFor
        }
        return axios.post(this.scrapeRoute, {
            request : request
        }).then(response => {
            return response.data;
        })
        .catch(error => {
            return error;
        })

    },

}