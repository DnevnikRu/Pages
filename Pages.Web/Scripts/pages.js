$(function () {
    window.Page = Backbone.Model.extend({
        defaults: function () {
            return {
                name: "new page",
                content: "empty **page**",
                renderedContent: ""
            };
        },
        initialize: function () {
            if (!this.get("name")) {
                this.set({ "name": this.defaults.title });
            }
        },
        renderContent: function () {
            this.set({ renderedContent: (new Showdown.converter()).makeHtml(this.get("content")) });
            return this;
        },
        idAttribute: 'name',
        urlRoot: "/pages"
    });

    var PagesCollection = Backbone.Collection.extend({
        model: Page,
        url: "/pages",
        fetchOne: function (name, success, error) {
            var page = new Page({ name: name });
            page.fetch({success:success, errror:error});
        }
    });

    window.Pages = new PagesCollection;

    var PageView = Backbone.View.extend({
        tagName: "article",
        template: _.template($('#page-template').html()),
        viewTemplate: _.template($('#page-view-template').html()),
        editTemplate: _.template($('#page-edit-template').html()),
        events: {
            "dblclick .view .name": "toggleEdit",
            "dblclick .view .content": "toggleEdit",
            "keyup .edit .name": "editPage",
            "keyup .edit .content": "editPage"
        },
        initialize: function () {
            this.model.bind('change', this.renderPage, this);
            this.model.bind('destroy', this.remove, this);
        },
        render: function () {
            this.$el.html(this.template());

            this.renderPage();

            this.$('.edit').hide();
            return this;
        },
        editPage: function () {
            this.model.set({ name: this.$('.edit .name').val() });
            this.model.set({ content: this.$('.edit .content').val() });
        },
        renderPage: function(){
            this.model.renderContent();
            this.$('.view').html(this.viewTemplate(this.model.toJSON()));
        },
        toggleEdit: function () {
            if (this.$('.edit').is(':visible')) {
                this.$('.edit').empty();
                this.$('.edit').hide();
            } else {
                this.$('.edit').html(this.editTemplate(this.model.toJSON()));
                this.$('.edit').show();
            }
        }
    });

    var PageStackView = Backbone.View.extend({
        pages: [],
        el: $('body'),
        initialize: function () {

        },
        pushPage: function(page) {
            this.pages.push(page);
            this.render();
        },
        render: function(){
            this.$el.empty();
            for (var i = 0; i < this.pages.length; i++) {
                var view = new PageView({ model: this.pages[i] });
                var article = view.render().$el;
                $(article).css("left", (i+1) * 15 + "px");
                $(article).css("top", (i+1) * 35 + "px");
                this.$el.append(article);
            }

            return this;
        },
        popPage: function () {
            this.pages.pop();
            this.render();
        },
        navigateToPage: function (name, success, error) {
            var page = Pages.fetchOne(name, PageStack.pushPage, PageStack.pushPage);
        }
    });

    window.PageStack = new PageStackView;

    var PageRouter = Backbone.Router.extend({
        routes: {
            "pages/:name": "navigateToPage",
            "*actions": "defaultRoute" // Backbone will try match the route above first
        }
    });

    var pageRouter = new PageRouter;

    pageRouter.on('route:navigateToPage', function (name) {
        PageStack.navigateToPage(name);
    });

    Backbone.history.start();

    PageStack.navigateToPage("index");
    
});