var ProfilesViewModel = function () {
    var self = this;
    var url = "/contact/GetAllProfiles";
    var refresh = function () {
        $.getJSON(url, {}, function (data) {
            self.Profiles(data);
        });
    };

    // Public data properties
    self.Profiles = ko.observableArray([]);

    // Public operations
    self.createProfile = function () {
        window.location.href = '/Contact/CreateEdit/0';
    };

    self.editProfile = function (profile) {
        window.location.href = '/Contact/CreateEdit/' + profile.ProfileId;
    };

    self.removeProfile = function (profile) {
        if (confirm("Are you sure you want to delete this profile?")) {
            var id = profile.ProfileId;
            waitingDialog({});
            $.ajax({
                type: 'DELETE', url: 'Contact/DeleteProfile/' + id,
                success: function () { self.Profiles.remove(profile); },
                error: function (err) {
                    var error = JSON.parse(err.responseText);
                    $("<div></div>").html(error.Message).dialog({ modal: true, title: "Error", buttons: { "Ok": function () { $(this).dialog("close"); } } }).show();
                },
                complete: function () { closeWaitingDialog(); }
            });
        }
    };
    refresh();
};
ko.applyBindings(new ProfilesViewModel());