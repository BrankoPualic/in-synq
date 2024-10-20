INSERT INTO LegalDocument (Title, Content, TypeId, [Version], [Language], CreatedOn, CreatedBy, LastChangedOn, LastChangedBy)
VALUES (
    'Privacy Policy',
    '<div class="bold">Introduction</div><div class="small">At InSynq, we are committed to protecting your privacy. This Privacy Policy explains how we collect, use, share, and protect your information when you use our social media application. By using InSynq, you agree to the terms outlined in this policy.</div>
    <div class="bold">Personal Information</div><div class="small">When you create an account, we may collect personal information such as your name, email address, phone number, date of birth, and profile photo.</div>
    <div class="bold">Content You Share</div><div class="small">Any posts, comments, photos, or other content you share on InSynq will be stored and displayed according to your privacy settings.</div>
    <div class="bold">Usage Data</div><div class="small">We collect data about your interactions with the app, including pages visited, features used, and time spent on the app.</div>
    <div class="bold">Device Information</div><div class="small">Information about your device, such as its operating system, browser type, IP address, and unique device identifiers.</div>
    <div class="bold">To Provide and Improve the Service</div><div class="small">We use your information to operate and enhance the InSynq app and its features.</div>
    <div class="bold">Personalization</div><div class="small">To personalize your experience, such as recommending content or suggesting connections.</div>
    <div class="bold">Communication</div><div class="small">To send you updates, notifications, and promotional content.</div>
    <div class="bold">With Other Users</div><div class="small">Depending on your privacy settings, some of your information (e.g., posts, profile) may be shared with other InSynq users.</div>
    <div class="bold">Service Providers</div><div class="small">We may share your information with third-party service providers who assist us in delivering the app.</div>
    <div class="bold">Legal Requirements</div><div class="small">We may disclose your information if required by law or to protect our rights.</div>
    <div class="bold">Account Settings</div><div class="small">You can update your account settings to control the visibility of your profile and content.</div>
    <div class="bold">Data Deletion</div><div class="small">You can delete your account, and we will remove your personal information from our active databases.</div>
    <div class="bold">Security</div><div class="small">To detect and prevent fraud or other malicious activity. We use reasonable administrative, technical, and physical security measures to protect your information.</div>
    <div class="bold">Changes to This Policy</div><div class="small">We may update this Privacy Policy from time to time. We will notify you of any significant changes.</div>
    <div class="bold">Contact Us</div><div class="small">If you have any questions about this Privacy Policy, please reach out to us through the Help Desk section.</div>',
    10, -- TypeId for Privacy Policy
    '1.0', -- Version
    'en', -- Language
    CURRENT_TIMESTAMP, -- CreatedOn,
    1,
    CURRENT_TIMESTAMP,  -- LastChangedOn,
    1
);

INSERT INTO LegalDocument (Title, Content, TypeId, [Version], [Language], CreatedOn, CreatedBy, LastChangedOn, LastChangedBy)
VALUES (
    'Terms of Use',
    '<div class="bold">Acceptance of Terms</div><div class="small">By accessing or using the InSynq application, you agree to be bound by these Terms of Use. If you do not agree to these terms, do not use the app.</div>
    <div class="bold">Account Security</div><div class="small">You are responsible for maintaining the confidentiality of your login credentials and for all activities that occur under your account.</div>
    <div class="bold">Acceptable Use</div><div class="small">You agree not to use InSynq for any unlawful purpose or to engage in any activity that violates the rights of others.</div>
    <div class="bold">User-Generated Content</div><div class="small">You retain ownership of the content you post on InSynq. By posting content, you grant us a non-exclusive, royalty-free license to use, display, and distribute it within the app.</div>
    <div class="bold">Prohibited Content</div><div class="small">Do not post content that is illegal, offensive, defamatory, or infringes on others'' rights.</div>
    <div class="bold">Intellectual Property</div><div class="small">All content provided by InSynq, including text, graphics, and logos, is the property of InSynq and protected by copyright laws.</div>
    <div class="bold">Termination</div><div class="small">We may suspend or terminate your account if you violate these Terms of Use or engage in prohibited activities.</div>
    <div class="bold">Limitation of Liability</div><div class="small">InSynq is provided "as is" and "as available." We do not guarantee that the app will be error-free or uninterrupted. We are not liable for any damages resulting from your use of the app.</div>
    <div class="bold">Changes to the Terms</div><div class="small">We reserve the right to update these Terms of Use at any time. Continued use of InSynq indicates acceptance of the revised terms.</div>
    <div class="bold">Governing Law</div><div class="small">These Terms of Use are governed by the laws of the United States of America.</div>
    <div class="bold">Contact Us</div><div class="small">For any questions about these Terms of Use, please reach out to us through the Help Desk section.</div>',
    11, -- TypeId for Terms of Use
    '1.0', -- Version
    'en', -- Language
    CURRENT_TIMESTAMP, -- CreatedOn,
    1,
    CURRENT_TIMESTAMP,  -- LastChangedOn,
    1
);