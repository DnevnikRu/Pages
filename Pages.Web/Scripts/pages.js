$(function () {

    var Page = Backbone.Model.extend({
        defaults: function () {
            return {
                name: "new page",
                content: "**empty** content",
            };
        },
        initialize: function () {
            if (!this.get("name")) {
                this.set({ "name": this.defaults.title });
            }
        },
        idAttribute: 'name',
        urlRoot: "/pages"
    });

    var PagesCollection = Backbone.Collection.extend({
        model: Page,
        url: "/pages",

        fetchOne: function (name) {
            var page = new Page({ name: name });
            return page.fetch();
        }
    });

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
            var viewData = _.extend(this.model.toJSON(), {
                html: (new Showdown.converter()).makeHtml(this.model.get("content"))
            });
            this.$('.view').html(this.viewTemplate(viewData));
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
        stack: [],
        el: $('body'),
        initialize: function () {

        },
        pushPage: function(newPage) {
            var existingPage = _.find(this.stack, function (page) { return page.get('name') == newPage.get('name'); });
            if (existingPage)
                this.stack = _.without(this.stack, existingPage);
            
            this.stack.push(newPage);
            this.render();
        },
        render: function(){
            this.$el.empty();
            for (var i = 0; i < this.stack.length; i++) {
                var view = new PageView({ model: this.stack[i] });
                var article = view.render().$el;
                $(article).css("left", (i+1) * 15 + "px");
                $(article).css("top", (i+1) * 35 + "px");
                this.$el.append(article);
            }

            return this;
        },
        popPage: function () {
            this.stack.pop();
            this.render();
        },
    });

    var PageRouter = Backbone.Router.extend({
        routes: {
            "pages/:name": "navigateToPage",
            "*actions": "defaultRoute" // Backbone will try match the route above first
        }
    });

    var app = {}
    app.pages = new PagesCollection;
    app.pageStack = new PageStackView;
    
    app.pageRouter = new PageRouter;
    app.pageRouter.on('route:navigateToPage', function (name) {
        
        app.pages.fetchOne(name).done(function (data) {
            app.pageStack.pushPage(new Page(data));
        }).error(function () {
            app.pageStack.pushPage(new Page({ name: name }));
        });

    });

    Backbone.history.start();

    app.pageRouter.navigate('pages/index', {trigger: true});
});